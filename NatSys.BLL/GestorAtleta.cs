

using System;
using System.Collections.Generic;
using NatSys.DAL;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class GestorAtleta
    {
        private static GestorAtleta _instancia;
        private readonly DALAtletas _atletaDAL;

        private GestorAtleta(string connectionString)
        {
            _atletaDAL = new DALAtletas(connectionString);
        }

        public static void Inicializar(string connectionString)
        {
            if (_instancia == null)
                _instancia = new GestorAtleta(connectionString);
        }

        public static GestorAtleta GetInstancia()
        {
            if (_instancia == null)
                throw new InvalidOperationException("GestorAtleta no fue inicializado.");
            return _instancia;
        }

        public List<Atleta> ObtenerTodos() => _atletaDAL.ObtenerTodos();

        public List<Atleta> GetAtletasActivos() => _atletaDAL.ObtenerActivos();

        public Atleta ObtenerPorId(int idAtleta) => _atletaDAL.ObtenerPorId(idAtleta);

        // RF-02: nombre, apellido y fecha de nacimiento son obligatorios
        public void AgregarAtleta(
            string nombre,
            string apellido,
            DateTime fechaNacimiento,
            string categoria,
            List<int> idsPruebasIniciales)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El nombre y el apellido son obligatorios.");

            var atleta = new Atleta
            {
                Nombre = nombre,
                Apellido = apellido,
                FechaNacimiento = fechaNacimiento,
                Categoria = categoria,
                Estado = "activo"
            };

            _atletaDAL.Agregar(atleta, idsPruebasIniciales);
        }

        public void ModificarAtleta(
            int idAtleta,
            string nombre,
            string apellido,
            DateTime fechaNacimiento,
            string categoria)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El nombre y el apellido son obligatorios.");

            _atletaDAL.Modificar(idAtleta, nombre, apellido, fechaNacimiento, categoria);
        }

        // Regla de negocio (CU-02, CA-02): si el atleta tiene marcas registradas,
        // no se puede eliminar fisicamente. La Vista debe atrapar esta excepcion
        // y ofrecer DesactivarAtleta como alternativa.
        public void EliminarAtleta(int idAtleta)
        {
            if (_atletaDAL.TieneMarcasAsociadas(idAtleta))
                throw new InvalidOperationException(
                    "No se puede eliminar un atleta con marcas registradas. Se lo puede desactivar en su lugar.");

            _atletaDAL.EliminarFisico(idAtleta);
        }

        // Baja logica: conserva el historial de marcas del atleta
        public void DesactivarAtleta(int idAtleta) => _atletaDAL.MarcarInactivo(idAtleta);

        public void AsignarPrueba(int idAtleta, int idPrueba) => _atletaDAL.AsignarPrueba(idAtleta, idPrueba);

        public void QuitarPrueba(int idAtleta, int idPrueba) => _atletaDAL.QuitarPrueba(idAtleta, idPrueba);
    }
}