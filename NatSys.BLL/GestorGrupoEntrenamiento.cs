// Proyecto: NatSys.BLL
// Iteracion 2 - RF-07 / CU-06 Gestionar grupos de entrenamiento
//
// Singleton, igual que el resto de los gestores: Program.cs lo inicializa
// una vez con la cadena de conexion y las pantallas usan GetInstancia().

using System;
using System.Collections.Generic;
using System.Linq;
using NatSys.DAL;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class GestorGrupoEntrenamiento
    {
        private static GestorGrupoEntrenamiento? _instancia;
        private readonly DALGruposEntrenamiento _grupoDAL;

        private GestorGrupoEntrenamiento(string connectionString)
        {
            _grupoDAL = new DALGruposEntrenamiento(connectionString);
        }

        public static void Inicializar(string connectionString)
        {
            if (_instancia == null)
                _instancia = new GestorGrupoEntrenamiento(connectionString);
        }

        public static GestorGrupoEntrenamiento GetInstancia()
        {
            if (_instancia == null)
                throw new InvalidOperationException("GestorGrupoEntrenamiento no fue inicializado.");
            return _instancia;
        }

        public List<GrupoEntrenamiento> ObtenerTodos() => _grupoDAL.ObtenerTodos();

        public GrupoEntrenamiento ObtenerPorId(int idGrupo) => _grupoDAL.ObtenerPorId(idGrupo);

        // Para el combo de entrenador a cargo (CU-06 paso 6). No estaba en el
        // diagrama de clases: hay que agregarlo ahi tambien.
        public List<Entrenador> ObtenerEntrenadoresActivos() => _grupoDAL.ObtenerEntrenadoresActivos();

        // CU-06 Nuevo (pasos 6 a 8)
        public void AgregarGrupo(string nombre, int idEntrenador, List<int> idsAtletas)
        {
            ValidarDatos(nombre, idEntrenador);
            ValidarAtletas(idsAtletas);

            if (_grupoDAL.ExisteNombre(nombre.Trim()))
                throw new ArgumentException("Ya existe un grupo con ese nombre.");

            var grupo = new GrupoEntrenamiento
            {
                Nombre = nombre.Trim(),
                IdEntrenador = idEntrenador,
                Estado = "activo"
            };

            _grupoDAL.Agregar(grupo, idsAtletas);
        }

        // CU-06 Modificar (pasos 11 y 12)
        public void ModificarGrupo(int idGrupo, string nombre, int idEntrenador, List<int> idsAtletas)
        {
            ValidarDatos(nombre, idEntrenador);
            ValidarAtletas(idsAtletas);

            if (_grupoDAL.ExisteNombre(nombre.Trim(), idGrupo))
                throw new ArgumentException("Ya existe otro grupo con ese nombre.");

            _grupoDAL.Modificar(idGrupo, nombre.Trim(), idEntrenador, idsAtletas);
        }

        // CU-06 Eliminar (pasos 14 y 15). CA-02: con trabajos cargados no se
        // puede. La Vista atrapa la excepcion y muestra el mensaje; si no hay
        // trabajos, la Vista ya pidio la confirmacion antes de llamar aca.
        public void EliminarGrupo(int idGrupo)
        {
            if (_grupoDAL.TieneTrabajosAsociados(idGrupo))
                throw new InvalidOperationException(
                    "No se puede eliminar el grupo porque tiene trabajos diarios cargados.");

            _grupoDAL.Eliminar(idGrupo);
        }

        // CU-06 CA-01: campos obligatorios
        private void ValidarDatos(string nombre, int idEntrenador)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del grupo es obligatorio.");

            if (idEntrenador <= 0)
                throw new ArgumentException("Tenés que seleccionar el entrenador a cargo.");
        }

        // Multiplicidad del diagrama de clases: un grupo tiene 1..* atletas
        private void ValidarAtletas(List<int> idsAtletas)
        {
            if (idsAtletas == null || !idsAtletas.Any())
                throw new ArgumentException("Tenés que seleccionar al menos un atleta para el grupo.");
        }
    }
}
