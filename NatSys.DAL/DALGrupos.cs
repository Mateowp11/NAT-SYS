
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NatSys.Entidades;

namespace NatSys.DAL
{
    public class DALGrupos
    {
        private readonly string _connectionString;

        public DALGrupos(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Grupo> ObtenerTodos()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Grupos.Include(g => g.Permisos).AsNoTracking().ToList();
        }

        public List<Permiso> ObtenerTodosLosPermisos()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Permisos.AsNoTracking().ToList();
        }

        public void Agregar(Grupo grupo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            contexto.Grupos.Add(grupo);
            contexto.SaveChanges();
        }

        public void Modificar(int idGrupo, string nombre, string descripcion)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var grupo = contexto.Grupos.Find(idGrupo);
            grupo.Nombre = nombre;
            grupo.Descripcion = descripcion;
            contexto.SaveChanges();
        }

        // Regla de negocio: no eliminar un grupo que todavia tiene usuarios asignados
        public bool TieneUsuariosAsociados(int idGrupo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Usuarios.Any(u => u.Grupos.Any(g => g.IdGrupo == idGrupo));
        }

        public void Eliminar(int idGrupo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var grupo = contexto.Grupos.Find(idGrupo);
            contexto.Grupos.Remove(grupo);
            contexto.SaveChanges();
        }

        public void AsignarPermiso(int idGrupo, int idPermiso)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var grupo = contexto.Grupos.Include(g => g.Permisos).First(g => g.IdGrupo == idGrupo);
            if (!grupo.Permisos.Any(p => p.IdPermiso == idPermiso))
            {
                var permiso = contexto.Permisos.Find(idPermiso);
                grupo.Permisos.Add(permiso);
            }
            contexto.SaveChanges();
        }

        public void QuitarPermiso(int idGrupo, int idPermiso)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var grupo = contexto.Grupos.Include(g => g.Permisos).First(g => g.IdGrupo == idGrupo);
            var permiso = grupo.Permisos.FirstOrDefault(p => p.IdPermiso == idPermiso);
            if (permiso != null) grupo.Permisos.Remove(permiso);
            contexto.SaveChanges();
        }
    }
}
