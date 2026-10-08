// Proyecto: NatSys.BLL
//
// Implementa RF-03 / CU-03 (Gestionar marcas): validaciones, regla del
// atleta inactivo, y deteccion automatica del Record Personal (RP).
//
// El RP es por atleta + prueba + largo de pileta: un 100 Crol en 25m y uno
// en 50m no se comparan entre si (en pileta corta se nada mas rapido).

using System;
using System.Collections.Generic;
using System.Linq;
using NatSys.DAL;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class GestorMarcas
    {
        private static GestorMarcas? _instancia;
        private readonly DALMarcas _marcaDAL;
        private readonly DALAtletas _atletaDAL; // para chequear si el atleta esta activo

        private GestorMarcas(string connectionString)
        {
            _marcaDAL = new DALMarcas(connectionString);
            _atletaDAL = new DALAtletas(connectionString);
        }

        public static void Inicializar(string connectionString)
        {
            if (_instancia == null)
                _instancia = new GestorMarcas(connectionString);
        }

        public static GestorMarcas GetInstancia()
        {
            if (_instancia == null)
                throw new InvalidOperationException("GestorMarcas no fue inicializado.");
            return _instancia;
        }

        public List<Marca> ObtenerTodas() => _marcaDAL.ObtenerTodas();

        public Marca? ObtenerPorId(int idMarca) => _marcaDAL.ObtenerPorId(idMarca);

        public void AgregarMarca(int idAtleta, string tipo, int idPrueba, int largoPileta,
                                 string tiempo, int? idTorneo, DateTime fecha)
        {
            ValidarDatos(tipo, largoPileta, tiempo, idTorneo);

            // CA-02: no se registran marcas para atletas inactivos
            var atleta = _atletaDAL.ObtenerPorId(idAtleta);
            if (atleta == null)
                throw new ArgumentException("El atleta seleccionado no existe.");
            if (!atleta.EstaActivo())
                throw new InvalidOperationException("No se pueden registrar marcas para atletas inactivos.");

            var marca = new Marca
            {
                IdAtleta = idAtleta,
                Tipo = tipo,
                IdPrueba = idPrueba,
                LargoPileta = largoPileta,
                Tiempo = NormalizarTiempo(tiempo),
                IdTorneo = idTorneo,
                Fecha = fecha,
                EsRecordPersonal = false // lo decide RecalcularRP, no el que carga
            };

            _marcaDAL.Agregar(marca);
            RecalcularRP(idAtleta, idPrueba, largoPileta);
        }

        public void ModificarMarca(int idMarca, string tipo, int idPrueba, int largoPileta,
                                   string tiempo, int? idTorneo, DateTime fecha)
        {
            ValidarDatos(tipo, largoPileta, tiempo, idTorneo);

            // La traemos ANTES de modificarla, para saber en que grupo
            // (prueba + pileta) estaba antes del cambio.
            var marcaAnterior = _marcaDAL.ObtenerPorId(idMarca);
            if (marcaAnterior == null)
                throw new ArgumentException("La marca que querés modificar no existe.");

            _marcaDAL.Modificar(idMarca, NormalizarTiempo(tiempo), tipo, idPrueba, largoPileta, idTorneo, fecha);

            // Recalcula el RP del grupo donde quedo la marca...
            RecalcularRP(marcaAnterior.IdAtleta, idPrueba, largoPileta);

            // ...y si se movio de prueba o de pileta, tambien el del grupo
            // donde estaba antes (puede que se haya llevado el RP de ahi).
            if (marcaAnterior.IdPrueba != idPrueba || marcaAnterior.LargoPileta != largoPileta)
                RecalcularRP(marcaAnterior.IdAtleta, marcaAnterior.IdPrueba, marcaAnterior.LargoPileta);
        }

        public void EliminarMarca(int idMarca)
        {
            // Hay que traerla ANTES de borrarla: despues ya no podriamos
            // saber de que atleta, prueba y pileta era.
            var marca = _marcaDAL.ObtenerPorId(idMarca);
            if (marca == null)
                throw new ArgumentException("La marca que querés eliminar no existe.");

            _marcaDAL.Eliminar(idMarca);
            RecalcularRP(marca.IdAtleta, marca.IdPrueba, marca.LargoPileta);
        }

        // Reglas de RF-03 / CU-03 que no dependen de la base de datos
        private void ValidarDatos(string tipo, int largoPileta, string tiempo, int? idTorneo)
        {
            // CA-01: formato del tiempo (mm:ss.cc o ss.cc). Reutilizamos la
            // validacion y el mensaje que ya tiene MotorPasajes en vez de copiarlos.
            if (!MotorPasajes.GetInstancia().ValidarFormatoTiempo(tiempo))
                throw new ArgumentException(MotorPasajes.MensajeFormatoTiempo);

            if (tipo != "oficial" && tipo != "test")
                throw new ArgumentException("El tipo de marca debe ser oficial o test.");

            if (largoPileta != 25 && largoPileta != 50)
                throw new ArgumentException("El largo de pileta debe ser 25 o 50 metros.");

            // CA-03: una marca oficial necesita torneo
            if (tipo == "oficial" && idTorneo == null)
                throw new ArgumentException("Las marcas oficiales requieren un torneo asociado.");
        }

        // Deja todos los tiempos guardados con el mismo formato mm:ss.cc
        // (por ejemplo "28.50" pasa a "00:28.50" y "1:05.30" a "01:05.30").
        // Para mostrarlos lindos en pantalla esta MotorPasajes.FormatearParaMostrar.
        private string NormalizarTiempo(string tiempo)
        {
            var motor = MotorPasajes.GetInstancia();
            return motor.ConvertirDeSegundos(motor.ConvertirASegundos(tiempo));
        }

        // Busca la marca mas rapida del grupo (atleta + prueba + pileta) y
        // la deja como unico RP. Sirve igual para alta, modificacion y baja,
        // y cubre CA-04: si es la primera marca, es la mas rapida del grupo
        // y queda como primer RP.
        private void RecalcularRP(int idAtleta, int idPrueba, int largoPileta)
        {
            var motor = MotorPasajes.GetInstancia();
            var marcas = _marcaDAL.ObtenerMarcasDePrueba(idAtleta, idPrueba, largoPileta);

            var mejorMarca = marcas
                .OrderBy(m => motor.ConvertirASegundos(m.Tiempo)) // la mas rapida primero
                .ThenBy(m => m.Fecha)  // si empatan, el RP es de la que se hizo primero
                .FirstOrDefault();     // null si no quedo ninguna marca

            _marcaDAL.MarcarRP(idAtleta, idPrueba, largoPileta, mejorMarca?.IdMarca);
        }
    }
}