// Proyecto: NatSys.BLL

using System;
using System.Collections.Generic;
using NatSys.DAL;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class GestorAtleta
    {
        private static GestorAtleta? _instancia;
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

        // CU-01 paso 4 (getPruebasAtleta en el diagrama de secuencias):
        // solo las pruebas asignadas a ese atleta, con su Estilo cargado
        public List<Prueba> ObtenerPruebasDeAtleta(int idAtleta) => _atletaDAL.ObtenerPruebasDeAtleta(idAtleta);

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

        // Regla de negocio (CU-02, CA-02): si el atleta tiene historial (marcas
        // o planes de pasajes guardados), no se puede eliminar fisicamente.
        // La Vista atrapa esta excepcion y ofrece DesactivarAtleta como
        // alternativa (baja logica, que conserva todo el historial).
        public void EliminarAtleta(int idAtleta)
        {
            // Un mensaje distinto para cada caso, asi el entrenador sabe que
            // tiene que borrar primero (o si le conviene desactivarlo)
            if (_atletaDAL.TieneMarcasAsociadas(idAtleta))
                throw new InvalidOperationException(
                    "No se puede eliminar: el atleta tiene marcas registradas.\n" +
                    "Si lo cargaste por error, borrá primero sus marcas desde Gestión de Marcas.");

            if (_atletaDAL.TienePlanesAsociados(idAtleta))
                throw new InvalidOperationException(
                    "No se puede eliminar: el atleta tiene planes de pasajes guardados.\n" +
                    "Si lo cargaste por error, borrá primero sus planes con el botón \"Planes\".");

            _atletaDAL.EliminarFisico(idAtleta);
        }

        // Baja logica: conserva el historial de marcas del atleta
        public void DesactivarAtleta(int idAtleta) => _atletaDAL.MarcarInactivo(idAtleta);

        // Lo contrario de la baja logica: se le vuelven a poder cargar marcas
        // y calcular pasajes, con todo su historial intacto
        public void ReactivarAtleta(int idAtleta) => _atletaDAL.MarcarActivo(idAtleta);

        public void AsignarPrueba(int idAtleta, int idPrueba) => _atletaDAL.AsignarPrueba(idAtleta, idPrueba);

        public void QuitarPrueba(int idAtleta, int idPrueba) => _atletaDAL.QuitarPrueba(idAtleta, idPrueba);
    }
}