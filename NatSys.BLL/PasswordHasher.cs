// Proyecto: NatSys.BLL / Seguridad
// No requiere ningun paquete NuGet: Rfc2898DeriveBytes viene en .NET (System.Security.Cryptography)

using System;
using System.Linq;
using System.Security.Cryptography;

namespace NatSys.BLL
{
    public static class PasswordHasher
    {
        private const int TamanioSalt = 16;   // 128 bits
        private const int TamanioHash = 32;   // 256 bits
        private const int Iteraciones = 100_000;

        // Genera el hash a guardar en la base de datos (para alta de usuario o cambio de clave)
        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(TamanioSalt);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password, salt, Iteraciones, HashAlgorithmName.SHA256, TamanioHash);

            // Guardamos todo junto separado por puntos: iteraciones.salt.hash
            // Asi si el dia de manana subimos las iteraciones, los usuarios viejos
            // se siguen pudiendo verificar con las iteraciones que tenian guardadas.
            return $"{Iteraciones}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        // Compara una clave ingresada contra el hash guardado. Devuelve true si coincide.
        public static bool VerifyPassword(string password, string hashAlmacenado)
        {
            var partes = hashAlmacenado.Split('.');
            if (partes.Length != 3) return false;

            int iteraciones = int.Parse(partes[0]);
            byte[] salt = Convert.FromBase64String(partes[1]);
            byte[] hashGuardado = Convert.FromBase64String(partes[2]);

            byte[] hashIngresado = Rfc2898DeriveBytes.Pbkdf2(
                password, salt, iteraciones, HashAlgorithmName.SHA256, hashGuardado.Length);

            // FixedTimeEquals compara en tiempo constante: evita que un atacante
            // pueda medir microsegundos de diferencia para deducir el hash caracter
            // por caracter (ataque de temporizacion). Nunca usar == para esto.
            return CryptographicOperations.FixedTimeEquals(hashIngresado, hashGuardado);
        }

        // Valida la regla de negocio RF-05: minimo 8 caracteres, letras y numeros
        public static bool CumpleRequisitos(string password, out string mensajeError)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                mensajeError = "La contraseña debe tener al menos 8 caracteres.";
                return false;
            }

            bool tieneLetra = password.Any(char.IsLetter);
            bool tieneNumero = password.Any(char.IsDigit);

            if (!tieneLetra || !tieneNumero)
            {
                mensajeError = "La contraseña debe combinar letras y números.";
                return false;
            }

            mensajeError = null;
            return true;
        }
    }
}