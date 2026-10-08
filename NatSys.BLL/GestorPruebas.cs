// Proyecto: NatSys.BLL
//
// Gestor de solo lectura. Aunque hoy solo "pasa" los datos del DAL, la
// UI igual tiene que entrar por aca y no ir directo al DAL: es la regla
// de capas (la UI nunca habla con la capa de datos).

using System;
using System.Collections.Generic;
using NatSys.DAL;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class GestorPruebas
    {
        private static GestorPruebas? _instancia;
        private readonly DALPruebas _pruebaDAL;

        private GestorPruebas(string connectionString)
        {
            _pruebaDAL = new DALPruebas(connectionString);
        }

        public static void Inicializar(string connectionString)
        {
            if (_instancia == null)
                _instancia = new GestorPruebas(connectionString);
        }

        public static GestorPruebas GetInstancia()
        {
            if (_instancia == null)
                throw new InvalidOperationException("GestorPruebas no fue inicializado.");
            return _instancia;
        }

        public List<Prueba> ObtenerTodas() => _pruebaDAL.ObtenerTodas();

        public Prueba? ObtenerPorId(int idPrueba) => _pruebaDAL.ObtenerPorId(idPrueba);
    }
}