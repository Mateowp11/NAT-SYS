// Proyecto: NatSys.Entidades
// Iteracion 2 - RF-08 / CU-07 Cargar trabajo diario

using System;
using System.Collections.Generic;
using System.Linq;

namespace NatSys.Entidades
{
    // Trabajo que el entrenador le asigna a un grupo para una fecha.
    // Puede traer el Excel con la sesion completa, los bloques de la serie
    // principal (sobre los que el atleta carga sus tiempos) o las dos cosas.
    public class TrabajoDiario
    {
        public const string TipoPileta = "Pileta";
        public const string TipoGimnasio = "Gimnasio";

        public int IdTrabajo { get; set; }
        public DateTime Fecha { get; set; } // solo importa el dia (la columna es "date")
        public string Tipo { get; set; } // TipoPileta / TipoGimnasio

        // Excel opcional. Se guarda DENTRO de la base (varbinary) y no como
        // una ruta a un archivo: asi lo puede abrir cualquier PC del
        // entrenador sin compartir carpetas.
        public string? ArchivoNombre { get; set; }
        public byte[]? Archivo { get; set; }

        public int IdGrupoEntrenamiento { get; set; }
        public GrupoEntrenamiento Grupo { get; set; }

        // Composicion: un bloque solo existe dentro de su trabajo. Por la
        // regla de composicion la lista esta en el todo (aca) y Bloque no
        // tiene una propiedad que apunte de vuelta al TrabajoDiario.
        public List<Bloque> Bloques { get; set; } = new List<Bloque>();

        // CA-01 de CU-07: el trabajo tiene que traer archivo, bloques o ambos
        public bool TieneContenido()
        {
            return TieneArchivo() || Bloques.Any();
        }

        // Se fija tambien en ArchivoNombre porque el listado de trabajos no
        // trae los bytes del Excel (ver DALTrabajosDiarios.ObtenerPorGrupo)
        public bool TieneArchivo()
        {
            return !string.IsNullOrEmpty(ArchivoNombre)
                || (Archivo != null && Archivo.Length > 0);
        }
    }
}
