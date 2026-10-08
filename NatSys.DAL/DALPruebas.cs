// Proyecto: NatSys.DAL
//
// Solo lectura: las pruebas son fijas por reglamento y se cargan de
// fabrica (HasData), no se crean ni se borran desde el sistema.

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NatSys.Entidades;

namespace NatSys.DAL
{
    public class DALPruebas
    {
        private readonly string _connectionString;

        public DALPruebas(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Incluye el Estilo porque MotorPasajes lo necesita para elegir
        // los coeficientes y para detectar si la prueba es Combinado.
        // Ordenadas por estilo y distancia para que se vean prolijas en
        // las listas de las pantallas.
        public List<Prueba> ObtenerTodas()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Pruebas
                .Include(p => p.Estilo)
                .OrderBy(p => p.IdEstilo)
                .ThenBy(p => p.Distancia)
                .AsNoTracking()
                .ToList();
        }

        public Prueba? ObtenerPorId(int idPrueba)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Pruebas
                .Include(p => p.Estilo)
                .AsNoTracking()
                .FirstOrDefault(p => p.IdPrueba == idPrueba);
        }
    }
}