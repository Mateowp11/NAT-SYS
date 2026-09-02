// Proyecto: NatSys.DAL

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NatSys.Entidades;

namespace NatSys.DAL
{
    public class DALUsuarios
    {
        private readonly string _connectionString;

        public DALUsuarios(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Usuario ObtenerPorNombreUsuario(string nombreUsuario)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Usuarios
                .Include(u => u.Persona)
                .Include(u => u.Grupos)
                    .ThenInclude(g => g.Permisos)
                .AsNoTracking()
                .FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
        }

        public List<Usuario> ObtenerTodos()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Usuarios
                .Include(u => u.Persona)
                .Include(u => u.Grupos)
                .AsNoTracking()
                .ToList();
        }

        // Crea un Entrenador nuevo Y su Usuario en el mismo paso - para
        // cuando no hay ningun Atleta existente al que asignarle el login
        // (por ejemplo, el propio entrenador del club, o un ayudante).
        public void CrearEntrenadorConUsuario(Entrenador entrenador, Usuario usuario)
        {
            using var contexto = new NatSysDbContext(_connectionString);

            contexto.Entrenadores.Add(entrenador);
            contexto.SaveChanges(); // aca EF ya genero el IdPersona

            usuario.IdPersona = entrenador.IdPersona;
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }

        // Atletas que todavia no tienen un Usuario asociado - para elegir
        // a quien darle acceso al sistema desde la pantalla de alta.
        public List<Atleta> ObtenerAtletasSinUsuario()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var idsConUsuario = contexto.Usuarios.Select(u => u.IdPersona).ToList();

            return contexto.Atletas
                .Where(a => !idsConUsuario.Contains(a.IdPersona))
                .AsNoTracking()
                .ToList();
        }

        public void CrearUsuario(Usuario usuario, List<int> idsGruposIniciales)
        {
            using var contexto = new NatSysDbContext(_connectionString);

            if (idsGruposIniciales != null && idsGruposIniciales.Count > 0)
            {
                usuario.Grupos = contexto.Grupos
                    .Where(g => idsGruposIniciales.Contains(g.IdGrupo))
                    .ToList();
            }

            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }

        public void ModificarUsuario(int idUsuario, string nombreUsuario, string estado)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var usuario = contexto.Usuarios.Find(idUsuario);
            usuario.NombreUsuario = nombreUsuario;
            usuario.Estado = estado;
            contexto.SaveChanges();
        }

        public void EliminarUsuario(int idUsuario)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var usuario = contexto.Usuarios.Find(idUsuario);
            contexto.Usuarios.Remove(usuario);
            contexto.SaveChanges();
        }

        public void AsignarGrupo(int idUsuario, int idGrupo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var usuario = contexto.Usuarios.Include(u => u.Grupos).First(u => u.IdUsuario == idUsuario);
            if (!usuario.Grupos.Any(g => g.IdGrupo == idGrupo))
            {
                var grupo = contexto.Grupos.Find(idGrupo);
                usuario.Grupos.Add(grupo);
            }
            contexto.SaveChanges();
        }

        public void QuitarGrupo(int idUsuario, int idGrupo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var usuario = contexto.Usuarios.Include(u => u.Grupos).First(u => u.IdUsuario == idUsuario);
            var grupo = usuario.Grupos.FirstOrDefault(g => g.IdGrupo == idGrupo);
            if (grupo != null) usuario.Grupos.Remove(grupo);
            contexto.SaveChanges();
        }

        public void RegistrarIntentoFallido(int idUsuario, bool bloquear)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var usuario = contexto.Usuarios.Find(idUsuario);
            usuario.IntentosFallidos++;
            if (bloquear) usuario.Estado = "bloqueado";
            contexto.SaveChanges();
        }

        public void RegistrarLoginExitoso(int idUsuario)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var usuario = contexto.Usuarios.Find(idUsuario);
            usuario.IntentosFallidos = 0;
            usuario.UltimoAcceso = DateTime.Now;
            contexto.SaveChanges();
        }

        public void ActualizarClave(int idUsuario, string nuevaClaveHasheada)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var usuario = contexto.Usuarios.Find(idUsuario);
            usuario.Clave = nuevaClaveHasheada;
            contexto.SaveChanges();
        }

        public void ResetearPasswordYDesbloquear(int idUsuario, string nuevaClaveHasheada)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var usuario = contexto.Usuarios.Find(idUsuario);
            usuario.Clave = nuevaClaveHasheada;
            usuario.IntentosFallidos = 0;
            usuario.Estado = "activo";
            contexto.SaveChanges();
        }

        public void ReactivarCuenta(int idUsuario)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var usuario = contexto.Usuarios.Find(idUsuario);
            usuario.Estado = "activo";
            usuario.IntentosFallidos = 0;
            contexto.SaveChanges();
        }

        public Sesion RegistrarSesion(int idUsuario)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var sesion = new Sesion
            {
                IdUsuario = idUsuario,
                Inicio = DateTime.Now,
                Estado = "activa"
            };
            contexto.Sesiones.Add(sesion);
            contexto.SaveChanges();
            return sesion;
        }

        public void CerrarSesion(int idSesion)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var sesion = contexto.Sesiones.Find(idSesion);
            sesion.Fin = DateTime.Now;
            sesion.Estado = "cerrada";
            contexto.SaveChanges();
        }
    }
}