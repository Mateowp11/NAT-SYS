// Proyecto: NatSys.DAL

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NatSys.Entidades;

namespace NatSys.DAL
{
    public class DALPlanesPasaje
    {
        private readonly string _connectionString;

        public DALPlanesPasaje(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Guarda el plan nuevo. Si ya existia un plan "Disponible" para el
        // mismo atleta+prueba, lo marca "Reemplazado" en vez de borrarlo -
        // asi se conserva el historial completo (pasos 30 a 35 de tu
        // diagrama de secuencias CU-01).
        public void GuardarPlan(PlanPasaje planNuevo)
        {
            using var contexto = new NatSysDbContext(_connectionString);

            var planExistente = contexto.PlanesPasaje
                .Include(p => p.Pasajes)
                .FirstOrDefault(p => p.IdAtleta == planNuevo.IdAtleta
                    && p.IdPrueba == planNuevo.IdPrueba
                    && p.Estado == "Disponible");

            if (planExistente != null)
            {
                planExistente.MarcarComoReemplazado();
            }

            contexto.PlanesPasaje.Add(planNuevo);
            contexto.SaveChanges();
        }

        public PlanPasaje ObtenerPlanVigente(int idAtleta, int idPrueba)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.PlanesPasaje
                .Include(p => p.Pasajes)
                .AsNoTracking()
                .FirstOrDefault(p => p.IdAtleta == idAtleta
                    && p.IdPrueba == idPrueba
                    && p.Estado == "Disponible");
        }

        // Historial completo (vigentes y reemplazados) para reportes de evolucion
        public List<PlanPasaje> ObtenerHistorial(int idAtleta, int idPrueba)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.PlanesPasaje
                .Include(p => p.Pasajes)
                .Where(p => p.IdAtleta == idAtleta && p.IdPrueba == idPrueba)
                .OrderByDescending(p => p.Fecha)
                .AsNoTracking()
                .ToList();
        }
    }
}