// Proyecto: NatSys.DAL
// Iteracion 2 - CU-07 Cargar trabajo diario (y la busqueda del trabajo del dia de CU-08)

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NatSys.Entidades;

namespace NatSys.DAL
{
    public class DALTrabajosDiarios
    {
        private readonly string _connectionString;

        public DALTrabajosDiarios(string connectionString)
        {
            _connectionString = connectionString;
        }

        // CU-07 paso 3: los trabajos de un grupo, del mas nuevo al mas viejo.
        //
        // OJO: el archivo (Excel o imagen) NO viaja en esta consulta. El "Select" arma cada
        // TrabajoDiario con todo menos los bytes del archivo: con muchos
        // trabajos cargados, traer todos los archivos solo para mostrar la
        // grilla seria lento (mas con la base en la nube). ArchivoNombre si
        // viene, y alcanza para saber si tiene archivo (TieneArchivo). Los
        // bytes se piden aparte con ObtenerArchivo, solo al abrirlo.
        public List<TrabajoDiario> ObtenerPorGrupo(int idGrupo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.TrabajosDiarios
                .Where(t => t.IdGrupoEntrenamiento == idGrupo)
                .OrderByDescending(t => t.Fecha)
                .Select(t => new TrabajoDiario
                {
                    IdTrabajo = t.IdTrabajo,
                    Fecha = t.Fecha,
                    Tipo = t.Tipo,
                    ArchivoNombre = t.ArchivoNombre,
                    IdGrupoEntrenamiento = t.IdGrupoEntrenamiento,
                    Bloques = t.Bloques.OrderBy(b => b.Orden).ToList()
                })
                .AsNoTracking()
                .ToList();
        }

        // CU-08 (getTrabajosConBloques en el diagrama de secuencias): los
        // trabajos de HOY de los grupos del atleta, solo los que tienen
        // bloques (un dia de gimnasio sin serie no tiene tiempos para cargar).
        // Trae el nombre del grupo para que, si hay doble turno, el atleta
        // sepa cual es cual. Tampoco trae el archivo.
        public List<TrabajoDiario> ObtenerTrabajosConBloques(List<int> idsGrupos, DateTime fecha)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var dia = fecha.Date;
            return contexto.TrabajosDiarios
                .Where(t => idsGrupos.Contains(t.IdGrupoEntrenamiento)
                         && t.Fecha == dia
                         && t.Bloques.Any())
                .Select(t => new TrabajoDiario
                {
                    IdTrabajo = t.IdTrabajo,
                    Fecha = t.Fecha,
                    Tipo = t.Tipo,
                    ArchivoNombre = t.ArchivoNombre,
                    IdGrupoEntrenamiento = t.IdGrupoEntrenamiento,
                    Grupo = new GrupoEntrenamiento
                    {
                        IdGrupoEntrenamiento = t.Grupo.IdGrupoEntrenamiento,
                        Nombre = t.Grupo.Nombre
                    },
                    Bloques = t.Bloques.OrderBy(b => b.Orden).ToList()
                })
                .AsNoTracking()
                .ToList();
        }

        // Agrega el trabajo y sus bloques en un solo SaveChanges: EF inserta
        // primero el trabajo, toma el IdTrabajo que genera la base y se lo
        // pone a cada bloque antes de insertarlos.
        public void Agregar(TrabajoDiario trabajo)
        {
            using var contexto = new NatSysDbContext(_connectionString);

            // Del grupo solo se usa el Id. Si viniera el objeto cargado, EF
            // intentaria insertarlo como un grupo nuevo. (El "!" le avisa al
            // compilador que el null es a proposito.)
            trabajo.Grupo = null!;

            contexto.TrabajosDiarios.Add(trabajo);
            contexto.SaveChanges();
        }

        // CU-07 CA-03: algun atleta ya cargo tiempos sobre algun bloque
        public bool TieneResultados(int idTrabajo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Bloques
                .Where(b => b.IdTrabajo == idTrabajo)
                .Any(b => b.Resultados.Any());
        }

        // Solo los bytes del archivo (null si el trabajo no tiene)
        public byte[]? ObtenerArchivo(int idTrabajo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.TrabajosDiarios
                .Where(t => t.IdTrabajo == idTrabajo)
                .Select(t => t.Archivo)
                .FirstOrDefault();
        }

        // CU-07 paso 16: la base borra en cascada los bloques, sus resultados
        // y los tiempos (composicion, configurado en el DbContext).
        public void Eliminar(int idTrabajo)
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var trabajo = contexto.TrabajosDiarios.Find(idTrabajo);
            contexto.TrabajosDiarios.Remove(trabajo);
            contexto.SaveChanges();
        }
    }
}
