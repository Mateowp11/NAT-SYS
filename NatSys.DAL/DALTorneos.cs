// Proyecto: NatSys.DAL

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NatSys.Entidades;

namespace NatSys.DAL
{
    public class DALTorneos
    {
        private readonly string _connectionString;

        public DALTorneos(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Torneo> ObtenerTodos()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Torneos.AsNoTracking().ToList();
        }

        public Torneo ObtenerPorId(int idTorneo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Torneos
                .Include(t => t.Marcas)
                .AsNoTracking()
                .FirstOrDefault(t => t.IdTorneo == idTorneo);
        }

        // RF-04: no puede existir un torneo con el mismo nombre y fecha.
        // idExcluir se usa al modificar, para no compararse contra si mismo.
        public bool ExisteDuplicado(string nombreTorneo, DateTime fecha, int? idExcluir = null)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Torneos.Any(t =>
                t.NombreTorneo == nombreTorneo &&
                t.Fecha.Date == fecha.Date &&
                (idExcluir == null || t.IdTorneo != idExcluir));
        }

        public void Agregar(Torneo torneo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            contexto.Torneos.Add(torneo);
            contexto.SaveChanges();
        }

        public void Modificar(int idTorneo, string nombreTorneo, DateTime fecha, string sede, int largoPileta)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var torneo = contexto.Torneos.Find(idTorneo);
            torneo.NombreTorneo = nombreTorneo;
            torneo.Fecha = fecha;
            torneo.Sede = sede;
            torneo.LargoPileta = largoPileta;
            contexto.SaveChanges();
        }

        // Regla de negocio RF-04: no se puede eliminar un torneo con marcas asociadas
        public bool TieneMarcasAsociadas(int idTorneo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Marcas.Any(m => m.IdTorneo == idTorneo);
        }

        public void EliminarFisico(int idTorneo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var torneo = contexto.Torneos.Find(idTorneo);
            contexto.Torneos.Remove(torneo);
            contexto.SaveChanges();
        }

        public void MarcarFinalizado(int idTorneo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var torneo = contexto.Torneos.Find(idTorneo);
            torneo.Estado = "finalizado";
            contexto.SaveChanges();
        }
    }
}