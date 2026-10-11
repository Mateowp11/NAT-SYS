// Proyecto: NatSys.DAL

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NatSys.Entidades;

namespace NatSys.DAL
{
    public class DALMarcas
    {
        private readonly string _connectionString;

        public DALMarcas(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Trae todas las marcas con su atleta, su prueba y su torneo
        // (tres Include, porque la pantalla va a mostrar datos de los tres),
        // ordenadas de la mas nueva a la mas vieja.
        public List<Marca> ObtenerTodas()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Marcas
                .Include(m => m.Atleta)
                .Include(m => m.Prueba)
                .Include(m => m.TorneoRegistro)
                .OrderByDescending(m => m.Fecha)
                .AsNoTracking()
                .ToList();
        }

        // Trae UNA sola marca por su Id, o null si no existe.
        // FirstOrDefault porque el Id es unico.
        public Marca? ObtenerPorId(int idMarca)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Marcas
                .Include(m => m.Atleta)
                .Include(m => m.Prueba)
                .Include(m => m.TorneoRegistro)
                .AsNoTracking()
                .FirstOrDefault(m => m.IdMarca == idMarca);
        }

        // Inserta una marca nueva. Despues del SaveChanges, EF completa
        // solo marca.IdMarca con el numero que le asigno la base.
        public void Agregar(Marca marca)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            contexto.Marcas.Add(marca);
            contexto.SaveChanges();
        }

        // Busca la marca con Find (queda "vigilada" por EF), le pisa cada
        // propiedad con el valor nuevo, y SaveChanges arma el UPDATE solo
        // con lo que cambio. El atleta no se modifica.
        public void Modificar(int idMarca, string tiempo, string tipo, int idPrueba,
                              int largoPileta, int? idTorneo, DateTime fecha)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var marca = contexto.Marcas.Find(idMarca);
            marca.Tiempo = tiempo;
            marca.Tipo = tipo;
            marca.IdPrueba = idPrueba;
            marca.LargoPileta = largoPileta;
            marca.IdTorneo = idTorneo;
            marca.Fecha = fecha;
            contexto.SaveChanges();
        }

        // Borra la marca: Find para traerla, Remove para marcarla como
        // "a borrar", SaveChanges para ejecutar el DELETE de verdad.
        public void Eliminar(int idMarca)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var marca = contexto.Marcas.Find(idMarca);
            contexto.Marcas.Remove(marca);
            contexto.SaveChanges();
        }

        // Trae TODAS las marcas de un atleta, en una prueba, en un largo de
        // pileta. La usa el Gestor para buscar cual es la mas rapida (RP).
        // Aca si va Where + ToList, porque esperamos varios resultados.
        public List<Marca> ObtenerMarcasDePrueba(int idAtleta, int idPrueba, int largoPileta)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Marcas
                .Where(m => m.IdAtleta == idAtleta
                    && m.IdPrueba == idPrueba
                    && m.LargoPileta == largoPileta)
                .AsNoTracking()
                .ToList();
        }

        // Deja como RP solo a la marca idMarcaRP, y a todas las demas de
        // ese atleta/prueba/pileta les saca la marca de RP.
        // idMarcaRP es "int?" (puede ser null) para el caso en que se
        // borraron todas las marcas y ya no queda ninguna para ser RP.
        public void MarcarRP(int idAtleta, int idPrueba, int largoPileta, int? idMarcaRP)
        {
            using var contexto = new NatSysDbContext(_connectionString);

            // Sin AsNoTracking: estas marcas SI las vamos a modificar
            var marcas = contexto.Marcas
                .Where(m => m.IdAtleta == idAtleta
                    && m.IdPrueba == idPrueba
                    && m.LargoPileta == largoPileta)
                .ToList();

            foreach (var marca in marcas)
            {
                // true solo para la elegida, false para todas las demas
                marca.EsRecordPersonal = (marca.IdMarca == idMarcaRP);
            }

            contexto.SaveChanges();
        }

        // Para el reporte de evolucion (RF-06.2): todas las marcas de un
        // atleta en una pileta, entre dos fechas, con su prueba y el estilo
        // de la prueba. Ordenadas de la mas vieja a la mas nueva.
        public List<Marca> ObtenerMarcasDeAtleta(int idAtleta, int largoPileta, DateTime desde, DateTime hasta)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Marcas
                .Include(m => m.Prueba)
                    .ThenInclude(p => p.Estilo)
                .Where(m => m.IdAtleta == idAtleta
                    && m.LargoPileta == largoPileta
                    && m.Fecha >= desde
                    && m.Fecha <= hasta)
                .OrderBy(m => m.Fecha)
                .AsNoTracking()
                .ToList();
        }
    }
}