// Proyecto: NatSys.DAL

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NatSys.Entidades;

namespace NatSys.DAL
{
    public class DALAtletas
    {
        private readonly string _connectionString;

        public DALAtletas(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Atleta> ObtenerTodos()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Atletas
                .Include(a => a.Pruebas)
                .AsNoTracking()
                .ToList();
        }

        public List<Atleta> ObtenerActivos()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Atletas
                .Include(a => a.Pruebas)
                .Where(a => a.Estado == "activo")
                .AsNoTracking()
                .ToList();
        }

        // Trae el atleta con sus pruebas y marcas, para la pantalla de detalle
        public Atleta ObtenerPorId(int idAtleta)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Atletas
                .Include(a => a.Pruebas)
                .Include(a => a.Marcas)
                .AsNoTracking()
                .FirstOrDefault(a => a.IdPersona == idAtleta);
        }

        // Al ser TPT, Add() inserta automaticamente en Personas Y en Atletas
        // en la misma operacion - no hace falta manejarlo a mano.
        public void Agregar(Atleta atleta, List<int> idsPruebasIniciales)
        {
            using var contexto = new NatSysDbContext(_connectionString);

            if (idsPruebasIniciales != null && idsPruebasIniciales.Count > 0)
            {
                atleta.Pruebas = contexto.Pruebas
                    .Where(p => idsPruebasIniciales.Contains(p.IdPrueba))
                    .ToList();
            }

            contexto.Atletas.Add(atleta);
            contexto.SaveChanges();
        }

        public void Modificar(int idAtleta, string nombre, string apellido, DateTime fechaNacimiento, string categoria)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var atleta = contexto.Atletas.Find(idAtleta);
            atleta.Nombre = nombre;
            atleta.Apellido = apellido;
            atleta.FechaNacimiento = fechaNacimiento;
            atleta.Categoria = categoria;
            contexto.SaveChanges();
        }

        // Regla de negocio RF-02: no se puede eliminar un atleta con marcas registradas
        public bool TieneMarcasAsociadas(int idAtleta)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Marcas.Any(m => m.IdAtleta == idAtleta);
        }

        public void EliminarFisico(int idAtleta)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var atleta = contexto.Atletas.Find(idAtleta);
            contexto.Atletas.Remove(atleta);
            contexto.SaveChanges();
        }

        public void MarcarInactivo(int idAtleta)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var atleta = contexto.Atletas.Find(idAtleta);
            atleta.Estado = "inactivo";
            contexto.SaveChanges();
        }

        public void AsignarPrueba(int idAtleta, int idPrueba)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var atleta = contexto.Atletas.Include(a => a.Pruebas).First(a => a.IdPersona == idAtleta);
            if (!atleta.Pruebas.Any(p => p.IdPrueba == idPrueba))
            {
                var prueba = contexto.Pruebas.Find(idPrueba);
                atleta.Pruebas.Add(prueba);
            }
            contexto.SaveChanges();
        }

        public void QuitarPrueba(int idAtleta, int idPrueba)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var atleta = contexto.Atletas.Include(a => a.Pruebas).First(a => a.IdPersona == idAtleta);
            var prueba = atleta.Pruebas.FirstOrDefault(p => p.IdPrueba == idPrueba);
            if (prueba != null) atleta.Pruebas.Remove(prueba);
            contexto.SaveChanges();
        }
    }
}