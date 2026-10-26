// Proyecto: NatSys.DAL
// Iteracion 2 - CU-06 Gestionar grupos de entrenamiento

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NatSys.Entidades;

namespace NatSys.DAL
{
    public class DALGruposEntrenamiento
    {
        private readonly string _connectionString;

        public DALGruposEntrenamiento(string connectionString)
        {
            _connectionString = connectionString;
        }

        // CU-06 paso 2: listado con entrenador y atletas (la grilla muestra
        // el nombre del entrenador y la cantidad de atletas)
        public List<GrupoEntrenamiento> ObtenerTodos()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.GruposEntrenamiento
                .Include(g => g.Entrenador)
                .Include(g => g.Atletas)
                .AsNoTracking()
                .OrderBy(g => g.Nombre)
                .ToList();
        }

        public GrupoEntrenamiento ObtenerPorId(int idGrupo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.GruposEntrenamiento
                .Include(g => g.Entrenador)
                .Include(g => g.Atletas)
                .AsNoTracking()
                .FirstOrDefault(g => g.IdGrupoEntrenamiento == idGrupo);
        }

        // Para el combo "Seleccionar entrenador..." del alta
        public List<Entrenador> ObtenerEntrenadoresActivos()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Entrenadores
                .Where(e => e.Estado == "activo")
                .AsNoTracking()
                .OrderBy(e => e.Apellido)
                .ThenBy(e => e.Nombre)
                .ToList();
        }

        // CU-08 (getGruposAtleta en el diagrama de secuencias): los grupos
        // en los que esta un atleta - puede ser mas de uno (doble turno)
        public List<GrupoEntrenamiento> ObtenerGruposDeAtleta(int idAtleta)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.GruposEntrenamiento
                .Where(g => g.Atletas.Any(a => a.IdPersona == idAtleta))
                .AsNoTracking()
                .ToList();
        }

        // excluirIdGrupo: al modificar, el propio grupo no cuenta como duplicado
        public bool ExisteNombre(string nombre, int excluirIdGrupo = 0)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.GruposEntrenamiento
                .Any(g => g.Nombre == nombre && g.IdGrupoEntrenamiento != excluirIdGrupo);
        }

        // Los atletas se buscan en ESTE mismo contexto antes de agregarlos:
        // asi EF sabe que ya existen y solo inserta las filas de la tabla
        // intermedia AtletaGrupoEntrenamiento (no intenta crear atletas nuevos).
        public void Agregar(GrupoEntrenamiento grupo, List<int> idsAtletas)
        {
            using var contexto = new NatSysDbContext(_connectionString);

            var atletas = contexto.Atletas
                .Where(a => idsAtletas.Contains(a.IdPersona))
                .ToList();

            foreach (var atleta in atletas)
                grupo.AgregarAtleta(atleta);

            contexto.GruposEntrenamiento.Add(grupo);
            contexto.SaveChanges();
        }

        // CU-06 pasos 11-12: cambia los datos y la conformacion del grupo.
        // Se compara la lista actual con la nueva: los que se destildaron se
        // quitan y los que se tildaron se agregan.
        public void Modificar(int idGrupo, string nombre, int idEntrenador, List<int> idsAtletas)
        {
            using var contexto = new NatSysDbContext(_connectionString);

            var grupo = contexto.GruposEntrenamiento
                .Include(g => g.Atletas)
                .First(g => g.IdGrupoEntrenamiento == idGrupo);

            grupo.Nombre = nombre;
            grupo.IdEntrenador = idEntrenador;

            var quitar = grupo.Atletas
                .Where(a => !idsAtletas.Contains(a.IdPersona))
                .ToList();
            foreach (var atleta in quitar)
                grupo.QuitarAtleta(atleta);

            var idsActuales = grupo.Atletas.Select(a => a.IdPersona).ToList();
            var agregar = contexto.Atletas
                .Where(a => idsAtletas.Contains(a.IdPersona) && !idsActuales.Contains(a.IdPersona))
                .ToList();
            foreach (var atleta in agregar)
                grupo.AgregarAtleta(atleta);

            contexto.SaveChanges();
        }

        // CU-06 CA-02: no se elimina un grupo con trabajos diarios cargados
        public bool TieneTrabajosAsociados(int idGrupo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.TrabajosDiarios.Any(t => t.IdGrupoEntrenamiento == idGrupo);
        }

        // Las filas de AtletaGrupoEntrenamiento se borran solas (cascada);
        // los atletas no se tocan.
        public void Eliminar(int idGrupo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var grupo = contexto.GruposEntrenamiento.Find(idGrupo);
            contexto.GruposEntrenamiento.Remove(grupo);
            contexto.SaveChanges();
        }
    }
}
