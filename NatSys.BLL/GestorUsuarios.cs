// Proyecto: NatSys.BLL / Seguridad
//
// Nota de diseño: AgregarUsuario recibe el Id de una Persona que YA existe
// (un Atleta o un Entrenador). Dar de alta al nadador/entrenador es
// responsabilidad de GestorAtleta (RF-02) - un paso previo y separado.
// Esto sigue el flujo real del club: primero se registra al nadador, y
// despues, si necesita acceso al sistema, se le crea su usuario y clave.

using System;
using System.Collections.Generic;
using NatSys.DAL;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class GestorUsuarios
    {
        private static GestorUsuarios _instancia;
        private readonly DALUsuarios _usuarioDAL;

        private GestorUsuarios(string connectionString)
        {
            _usuarioDAL = new DALUsuarios(connectionString);
        }

        public static void Inicializar(string connectionString)
        {
            if (_instancia == null)
                _instancia = new GestorUsuarios(connectionString);
        }

        public static GestorUsuarios GetInstancia()
        {
            if (_instancia == null)
                throw new InvalidOperationException("GestorUsuarios no fue inicializado.");
            return _instancia;
        }

        public List<Usuario> ObtenerTodos() => _usuarioDAL.ObtenerTodos();

        public void AgregarUsuario(
            int idPersona,
            string nombreUsuario,
            string claveInicial,
            string preguntaSeguridad,
            string respuestaSeguridad,
            List<int> idsGruposIniciales)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario es obligatorio.");

            if (_usuarioDAL.ObtenerPorNombreUsuario(nombreUsuario) != null)
                throw new ArgumentException("Ya existe un usuario con ese nombre.");

            if (!PasswordHasher.CumpleRequisitos(claveInicial, out string mensajeError))
                throw new ArgumentException(mensajeError);

            var usuario = new Usuario
            {
                IdPersona = idPersona,
                NombreUsuario = nombreUsuario,
                Clave = PasswordHasher.HashPassword(claveInicial),
                Estado = "activo",
                IntentosFallidos = 0,
                PreguntaSeguridad = preguntaSeguridad,
                RespuestaSeguridadHash = PasswordHasher.HashPassword(
                    respuestaSeguridad.Trim().ToLowerInvariant())
            };

            _usuarioDAL.CrearUsuario(usuario, idsGruposIniciales);
        }

        // Crea un Entrenador nuevo junto con su Usuario, para cuando no
        // hay un Atleta existente al que asignarle el acceso.
        public void AgregarUsuarioEntrenador(
            string nombre,
            string apellido,
            string especialidad,
            string nombreUsuario,
            string claveInicial,
            string preguntaSeguridad,
            string respuestaSeguridad)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El nombre y el apellido son obligatorios.");

            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario es obligatorio.");

            if (_usuarioDAL.ObtenerPorNombreUsuario(nombreUsuario) != null)
                throw new ArgumentException("Ya existe un usuario con ese nombre.");

            if (!PasswordHasher.CumpleRequisitos(claveInicial, out string mensajeError))
                throw new ArgumentException(mensajeError);

            var entrenador = new Entrenador
            {
                Nombre = nombre,
                Apellido = apellido,
                Especialidad = especialidad,
                Estado = "activo"
            };

            var usuario = new Usuario
            {
                NombreUsuario = nombreUsuario,
                Clave = PasswordHasher.HashPassword(claveInicial),
                Estado = "activo",
                IntentosFallidos = 0,
                PreguntaSeguridad = preguntaSeguridad,
                RespuestaSeguridadHash = PasswordHasher.HashPassword(
                    respuestaSeguridad.Trim().ToLowerInvariant())
            };

            _usuarioDAL.CrearEntrenadorConUsuario(entrenador, usuario);
        }

        public List<Atleta> ObtenerAtletasSinUsuario() => _usuarioDAL.ObtenerAtletasSinUsuario();

        public void ModificarUsuario(int idUsuario, string nombreUsuario, string estado)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario es obligatorio.");

            _usuarioDAL.ModificarUsuario(idUsuario, nombreUsuario, estado);
        }

        public void EliminarUsuario(int idUsuario)
        {
            _usuarioDAL.EliminarUsuario(idUsuario);
        }

        // Reseteo por el Administrador: a diferencia de CambiarContraseña
        // (autoservicio, en GestorSeguridad), este NO pide la clave actual
        // -el administrador tiene la potestad de forzar una nueva- y de
        // paso desbloquea la cuenta si estaba bloqueada.
        public void ResetearClave(int idUsuario, string claveNueva)
        {
            if (!PasswordHasher.CumpleRequisitos(claveNueva, out string mensajeError))
                throw new ArgumentException(mensajeError);

            _usuarioDAL.ActualizarClave(idUsuario, PasswordHasher.HashPassword(claveNueva));
            _usuarioDAL.ReactivarCuenta(idUsuario);
        }

        public void AsignarGrupo(int idUsuario, int idGrupo) => _usuarioDAL.AsignarGrupo(idUsuario, idGrupo);

        public void QuitarGrupo(int idUsuario, int idGrupo) => _usuarioDAL.QuitarGrupo(idUsuario, idGrupo);
    }
}