// Proyecto: NatSys.BLL / Seguridad

using System;
using System.Collections.Generic;
using NatSys.DAL;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class GestorGrupos
    {
        private static GestorGrupos _instancia;
        private readonly DALGrupos _grupoDAL;

        private GestorGrupos(string connectionString)
        {
            _grupoDAL = new DALGrupos(connectionString);
        }

        public static void Inicializar(string connectionString)
        {
            if (_instancia == null)
                _instancia = new GestorGrupos(connectionString);
        }

        public static GestorGrupos GetInstancia()
        {
            if (_instancia == null)
                throw new InvalidOperationException("GestorGrupos no fue inicializado.");
            return _instancia;
        }

        public List<Grupo> ObtenerTodos() => _grupoDAL.ObtenerTodos();

        public List<Permiso> ObtenerPermisosDisponibles() => _grupoDAL.ObtenerTodosLosPermisos();

        public void AgregarGrupo(string nombre, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del grupo es obligatorio.");

            _grupoDAL.Agregar(new Grupo
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Estado = "activo"
            });
        }

        public void ModificarGrupo(int idGrupo, string nombre, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del grupo es obligatorio.");

            _grupoDAL.Modificar(idGrupo, nombre, descripcion);
        }

        // Regla de negocio: no se puede eliminar un grupo con usuarios asignados
        public void EliminarGrupo(int idGrupo)
        {
            if (_grupoDAL.TieneUsuariosAsociados(idGrupo))
                throw new InvalidOperationException(
                    "No se puede eliminar un grupo que tiene usuarios asignados.");

            _grupoDAL.Eliminar(idGrupo);
        }

        public void AsignarPermiso(int idGrupo, int idPermiso) => _grupoDAL.AsignarPermiso(idGrupo, idPermiso);

        public void QuitarPermiso(int idGrupo, int idPermiso) => _grupoDAL.QuitarPermiso(idGrupo, idPermiso);
    }
}