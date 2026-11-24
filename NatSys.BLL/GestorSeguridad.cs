// Proyecto: NatSys.BLL / Seguridad
// Firmas de metodos ajustadas EXACTO a tu diagrama de clases:
//   +getInstancia(): GestorSeguridad
//   +iniciarSesion(txtUsuario: string, txtClave: string): Sesion
//   +cerrarSesion(sesion: Sesion): void
//   +cambiarContraseña(u: Usuario, claveActual: string, nuevaClave: string): void
//     (ahora tambien pide la clave actual: hay que actualizarlo en el diagrama)
//   +verificarPermiso(u: Usuario, codigoPermiso: string): bool
//   -verificarIntentosFallidos(u: Usuario): void   <- privado en tu diagrama
//
// Como iniciarSesion devuelve Sesion (no un objeto "resultado" con mensaje),
// los casos de error de tu CU-SEG-01 (CA-01 a CA-03) se resuelven con
// excepciones personalizadas. La Vista las atrapa con try/catch y muestra
// ex.Message. Esto es un patron muy comun en C# para este tipo de metodo.

using System;
using System.Linq;
using NatSys.DAL;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class GestorSeguridad
    {
        private static GestorSeguridad? _instancia;
        private readonly DALUsuarios _usuarioDAL;
        private const int MaxIntentosFallidos = 3;

        // Privado, tal cual tu diagrama ("-sesionActual: Sesion"). La Vista
        // no lo consulta directo: se queda con la Sesion que le devuelve
        // iniciarSesion() y la va pasando entre formularios.
        private Sesion sesionActual;

        private GestorSeguridad(string connectionString)
        {
            _usuarioDAL = new DALUsuarios(connectionString);
        }

        // Se llama UNA sola vez al arrancar la app (Program.cs), pasando la
        // cadena de conexion de ESTE cliente instalado.
        public static void Inicializar(string connectionString)
        {
            if (_instancia == null)
                _instancia = new GestorSeguridad(connectionString);
        }

        public static GestorSeguridad GetInstancia()
        {
            if (_instancia == null)
                throw new InvalidOperationException(
                    "GestorSeguridad no fue inicializado. Llama a Inicializar() al arrancar la app.");
            return _instancia;
        }

        public Sesion IniciarSesion(string txtUsuario, string txtClave)
        {
            // CA-04: campos vacios
            if (string.IsNullOrWhiteSpace(txtUsuario) || string.IsNullOrWhiteSpace(txtClave))
                throw new CredencialesInvalidasException("Usuario y contraseña son obligatorios.");

            var usuario = _usuarioDAL.ObtenerPorNombreUsuario(txtUsuario);

            // Usuario no existe: mensaje generico (no revelamos si el dato
            // erroneo fue el usuario o la clave)
            if (usuario == null)
                throw new CredencialesInvalidasException("Usuario o contraseña incorrectos.");

            // CA-03: la cuenta ya estaba bloqueada de antes
            if (usuario.Estado == "bloqueado")
                throw new CuentaBloqueadaException(
                    "Tu cuenta está bloqueada. Contactá al administrador o recuperá tu contraseña.");

            // CA-01: contraseña incorrecta -> delega en el metodo privado
            if (!PasswordHasher.VerifyPassword(txtClave, usuario.Clave))
            {
                VerificarIntentosFallidos(usuario); // incrementa, bloquea si corresponde, y lanza la excepcion
            }

            // Login exitoso
            _usuarioDAL.RegistrarLoginExitoso(usuario.IdUsuario);
            var sesion = _usuarioDAL.RegistrarSesion(usuario.IdUsuario);
            sesion.Usuario = usuario;

            sesionActual = sesion;
            return sesion;
        }

        // Metodo privado: encapsula la logica de intentos fallidos y bloqueo,
        // tal cual esta modelado en tu diagrama (con el signo "-").
        private void VerificarIntentosFallidos(Usuario usuario)
        {
            int nuevoIntentos = usuario.IntentosFallidos + 1;
            bool debeBloquear = nuevoIntentos >= MaxIntentosFallidos;

            _usuarioDAL.RegistrarIntentoFallido(usuario.IdUsuario, debeBloquear);

            // CA-02: se acaba de superar el maximo de intentos
            if (debeBloquear)
                throw new CuentaBloqueadaException(
                    "Superaste el máximo de intentos. Tu cuenta fue bloqueada por seguridad.");

            throw new CredencialesInvalidasException(
                $"Usuario o contraseña incorrectos. Te quedan {MaxIntentosFallidos - nuevoIntentos} intento(s).");
        }

        public void CerrarSesion(Sesion sesion)
        {
            if (sesion == null) return;

            _usuarioDAL.CerrarSesion(sesion.IdSesion);

            if (sesionActual != null && sesionActual.IdSesion == sesion.IdSesion)
                sesionActual = null;
        }

        // RF-05.5: el usuario que inicio sesion cambia su propia contraseña.
        // Pide la clave actual: si alguien encuentra la sesion abierta en una
        // PC, no puede cambiarla sin saberla. Los errores se avisan con
        // CredencialesInvalidasException, igual que en el login, y la
        // pantalla muestra ex.Message.
        public void CambiarContraseña(Usuario usuario, string claveActual, string nuevaClave)
        {
            if (string.IsNullOrEmpty(claveActual) || string.IsNullOrEmpty(nuevaClave))
                throw new CredencialesInvalidasException("Completá la contraseña actual y la nueva.");

            // Se lee de la base y no de la sesion, por si la clave cambio
            // despues de iniciar sesion
            var usuarioBase = _usuarioDAL.ObtenerPorNombreUsuario(usuario.NombreUsuario);
            if (usuarioBase == null || !PasswordHasher.VerifyPassword(claveActual, usuarioBase.Clave))
                throw new CredencialesInvalidasException("La contraseña actual no es correcta.");

            if (PasswordHasher.VerifyPassword(nuevaClave, usuarioBase.Clave))
                throw new CredencialesInvalidasException("La contraseña nueva tiene que ser distinta de la actual.");

            // Mismas reglas que en todo el sistema: 8 caracteres, letras y numeros
            if (!PasswordHasher.CumpleRequisitos(nuevaClave, out string mensajeError))
                throw new CredencialesInvalidasException(mensajeError);

            string hashNueva = PasswordHasher.HashPassword(nuevaClave);
            _usuarioDAL.ActualizarClave(usuarioBase.IdUsuario, hashNueva);
            usuario.Clave = hashNueva; // la sesion abierta queda al dia
        }

        // Un usuario tiene el permiso si ALGUNO de sus grupos lo incluye
        // (RF-05: los permisos se asignan a nivel de grupo)
        public bool VerificarPermiso(Usuario usuario, string codigoPermiso)
        {
            return usuario.Grupos
                .SelectMany(g => g.Permisos)
                .Any(p => p.Nombre == codigoPermiso);
        }

        // --- Extension no presente en tu diagrama actual ---
        // Recuperacion de clave por pregunta de seguridad (CU-SEG-05).
        // Como todavia no esta dibujada en tu UML, te recomiendo agregarla
        // ahi tambien para que diagrama y codigo queden sincronizados.

        public string ObtenerPreguntaSeguridad(string nombreUsuario)
        {
            var usuario = _usuarioDAL.ObtenerPorNombreUsuario(nombreUsuario);
            return usuario?.PreguntaSeguridad;
        }

        public void RecuperarContraseña(string nombreUsuario, string respuesta, string claveNueva)
        {
            var usuario = _usuarioDAL.ObtenerPorNombreUsuario(nombreUsuario);

            if (usuario == null || string.IsNullOrWhiteSpace(usuario.RespuestaSeguridadHash))
                throw new CredencialesInvalidasException("No se pudo verificar la identidad.");

            string respuestaNormalizada = respuesta.Trim().ToLowerInvariant();

            if (!PasswordHasher.VerifyPassword(respuestaNormalizada, usuario.RespuestaSeguridadHash))
            {
                VerificarIntentosFallidos(usuario); // mismo mecanismo de bloqueo que el login
            }

            if (!PasswordHasher.CumpleRequisitos(claveNueva, out string mensajeError))
                throw new CredencialesInvalidasException(mensajeError);

            string hashNueva = PasswordHasher.HashPassword(claveNueva);
            _usuarioDAL.ResetearPasswordYDesbloquear(usuario.IdUsuario, hashNueva);
        }
    }
}