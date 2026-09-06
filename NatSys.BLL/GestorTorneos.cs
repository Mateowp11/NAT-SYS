// Proyecto: NatSys.BLL

using System;
using System.Collections.Generic;
using NatSys.DAL;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class GestorTorneos
    {
        private static GestorTorneos? _instancia;
        private readonly DALTorneos _torneoDAL;

        private GestorTorneos(string connectionString)
        {
            _torneoDAL = new DALTorneos(connectionString);
        }

        public static void Inicializar(string connectionString)
        {
            if (_instancia == null)
                _instancia = new GestorTorneos(connectionString);
        }

        public static GestorTorneos GetInstancia()
        {
            if (_instancia == null)
                throw new InvalidOperationException("GestorTorneos no fue inicializado.");
            return _instancia;
        }

        public List<Torneo> ObtenerTodos() => _torneoDAL.ObtenerTodos();

        public Torneo ObtenerPorId(int idTorneo) => _torneoDAL.ObtenerPorId(idTorneo);

        // RF-04: nombre, fecha y largo de pileta son obligatorios; no se
        // permite un torneo duplicado (mismo nombre y fecha) - CA-01
        public void AgregarTorneo(string nombreTorneo, DateTime fecha, string sede, int largoPileta)
        {
            ValidarDatos(nombreTorneo, largoPileta);

            if (_torneoDAL.ExisteDuplicado(nombreTorneo, fecha))
                throw new ArgumentException("Ya existe un torneo con ese nombre y fecha.");

            var torneo = new Torneo
            {
                NombreTorneo = nombreTorneo,
                Fecha = fecha,
                Sede = sede,
                LargoPileta = largoPileta,
                Estado = "activo"
            };

            _torneoDAL.Agregar(torneo);
        }

        public void ModificarTorneo(int idTorneo, string nombreTorneo, DateTime fecha, string sede, int largoPileta)
        {
            ValidarDatos(nombreTorneo, largoPileta);

            if (_torneoDAL.ExisteDuplicado(nombreTorneo, fecha, idTorneo))
                throw new ArgumentException("Ya existe otro torneo con ese nombre y fecha.");

            _torneoDAL.Modificar(idTorneo, nombreTorneo, fecha, sede, largoPileta);
        }

        private void ValidarDatos(string nombreTorneo, int largoPileta)
        {
            if (string.IsNullOrWhiteSpace(nombreTorneo))
                throw new ArgumentException("El nombre del torneo es obligatorio.");

            if (largoPileta != 25 && largoPileta != 50)
                throw new ArgumentException("El largo de pileta debe ser 25 o 50 metros.");
        }

        // RF-04 CA-02: si tiene marcas asociadas, no se puede eliminar -
        // la Vista debe atrapar esta excepcion y ofrecer FinalizarTorneo
        public void EliminarTorneo(int idTorneo)
        {
            if (_torneoDAL.TieneMarcasAsociadas(idTorneo))
                throw new InvalidOperationException(
                    "No se puede eliminar un torneo con marcas asociadas. Se lo puede marcar como finalizado en su lugar.");

            _torneoDAL.EliminarFisico(idTorneo);
        }

        // Baja logica: conserva el historial de marcas del torneo
        public void FinalizarTorneo(int idTorneo) => _torneoDAL.MarcarFinalizado(idTorneo);
    }
}