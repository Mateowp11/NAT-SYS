// Proyecto: NatSys.BLL
// Iteracion 2 - RF-08 / CU-07 Cargar trabajo diario
//               (+ GetTrabajosDelDia, el primer paso de CU-08)

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NatSys.DAL;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class GestorTrabajoDiario
    {
        private static GestorTrabajoDiario? _instancia;
        private readonly DALTrabajosDiarios _trabajoDAL;
        private readonly DALGruposEntrenamiento _grupoDAL;

        private static readonly string[] ExtensionesExcel = { ".xlsx", ".xls", ".xlsm" };

        private GestorTrabajoDiario(string connectionString)
        {
            _trabajoDAL = new DALTrabajosDiarios(connectionString);
            _grupoDAL = new DALGruposEntrenamiento(connectionString);
        }

        public static void Inicializar(string connectionString)
        {
            if (_instancia == null)
                _instancia = new GestorTrabajoDiario(connectionString);
        }

        public static GestorTrabajoDiario GetInstancia()
        {
            if (_instancia == null)
                throw new InvalidOperationException("GestorTrabajoDiario no fue inicializado.");
            return _instancia;
        }

        // CU-07 paso 3 (sin los bytes del Excel, ver DALTrabajosDiarios)
        public List<TrabajoDiario> ObtenerTrabajosPorGrupo(int idGrupo) => _trabajoDAL.ObtenerPorGrupo(idGrupo);

        // CU-07 Agregar (pasos 7 a 11). La pantalla arma el TrabajoDiario con
        // la fecha, el tipo, el archivo (si hay) y los bloques en el orden en
        // que el entrenador los cargo.
        public void AgregarTrabajo(TrabajoDiario trabajo)
        {
            ValidarContenido(trabajo);

            trabajo.Fecha = trabajo.Fecha.Date; // por las dudas: solo el dia

            // El orden de los bloques es el orden de la lista (1, 2, 3...)
            for (int i = 0; i < trabajo.Bloques.Count; i++)
                trabajo.Bloques[i].Orden = i + 1;

            _trabajoDAL.Agregar(trabajo);
        }

        // CU-07 CA-03: la Vista lo consulta ANTES de eliminar, para advertir
        // que se van a perder los tiempos que ya cargaron los atletas
        public bool TieneResultadosCargados(int idTrabajo) => _trabajoDAL.TieneResultados(idTrabajo);

        // CU-07 Eliminar (paso 16): se borran el trabajo, sus bloques y los
        // resultados (cascada). La confirmacion la pide la Vista.
        public void EliminarTrabajo(int idTrabajo) => _trabajoDAL.Eliminar(idTrabajo);

        // CU-07 Abrir (pasos 12-13). CA-02: si no tiene archivo, se avisa.
        public byte[] ObtenerArchivo(int idTrabajo)
        {
            var archivo = _trabajoDAL.ObtenerArchivo(idTrabajo);

            if (archivo == null || archivo.Length == 0)
                throw new InvalidOperationException("Este trabajo no tiene un archivo adjunto.");

            return archivo;
        }

        // CU-08 pasos 2-3: los trabajos con bloques de HOY en cualquiera de
        // los grupos del atleta. Lista vacia = CA-01 (no hay nada para cargar).
        // Mas de uno = doble turno: la pantalla le pide que elija.
        public List<TrabajoDiario> GetTrabajosDelDia(int idAtleta, DateTime fecha)
        {
            var grupos = _grupoDAL.ObtenerGruposDeAtleta(idAtleta);

            if (!grupos.Any())
                return new List<TrabajoDiario>();

            var idsGrupos = grupos.Select(g => g.IdGrupoEntrenamiento).ToList();
            return _trabajoDAL.ObtenerTrabajosConBloques(idsGrupos, fecha);
        }

        // CU-07 paso 10 y CA-01, mas los datos minimos de cada bloque
        private void ValidarContenido(TrabajoDiario trabajo)
        {
            if (trabajo == null)
                throw new ArgumentNullException(nameof(trabajo));

            if (trabajo.IdGrupoEntrenamiento <= 0)
                throw new ArgumentException("Tenés que seleccionar el grupo.");

            if (trabajo.Fecha == default)
                throw new ArgumentException("La fecha del trabajo es obligatoria.");

            if (trabajo.Tipo != TrabajoDiario.TipoPileta && trabajo.Tipo != TrabajoDiario.TipoGimnasio)
                throw new ArgumentException("El tipo de trabajo tiene que ser Pileta o Gimnasio.");

            // CA-01
            if (!trabajo.TieneContenido())
                throw new ArgumentException("Tenés que adjuntar el archivo del trabajo o cargar al menos un bloque.");

            // Los dias de gimnasio no tienen serie principal: no hay tiempos que cargar
            if (trabajo.Tipo == TrabajoDiario.TipoGimnasio && trabajo.Bloques.Any())
                throw new ArgumentException("Un trabajo de gimnasio no lleva bloques: adjuntá solo el archivo.");

            if (trabajo.TieneArchivo())
                ValidarArchivo(trabajo);

            for (int i = 0; i < trabajo.Bloques.Count; i++)
                ValidarBloque(trabajo.Bloques[i], i + 1);
        }

        private void ValidarArchivo(TrabajoDiario trabajo)
        {
            if (trabajo.Archivo == null || trabajo.Archivo.Length == 0)
                throw new ArgumentException("El archivo adjunto está vacío.");

            if (string.IsNullOrWhiteSpace(trabajo.ArchivoNombre))
                throw new ArgumentException("Falta el nombre del archivo adjunto.");

            string extension = Path.GetExtension(trabajo.ArchivoNombre).ToLowerInvariant();
            if (!ExtensionesExcel.Contains(extension))
                throw new ArgumentException("El archivo tiene que ser un Excel (.xlsx o .xls).");
        }

        private void ValidarBloque(Bloque bloque, int numero)
        {
            if (string.IsNullOrWhiteSpace(bloque.Descripcion))
                throw new ArgumentException($"Bloque {numero}: la descripción es obligatoria.");

            if (bloque.Distancia <= 0)
                throw new ArgumentException($"Bloque {numero}: la distancia tiene que ser mayor a 0.");

            if (bloque.Repeticiones <= 0)
                throw new ArgumentException($"Bloque {numero}: la cantidad de repeticiones tiene que ser mayor a 0.");
        }
    }
}
