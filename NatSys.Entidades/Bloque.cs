// Proyecto: NatSys.Entidades
// Iteracion 2 - parte de la serie principal de un TrabajoDiario

using System;
using System.Collections.Generic;
using System.Linq;

namespace NatSys.Entidades
{
    // Un tramo de la serie principal con la misma distancia y descripcion.
    // Ejemplo: la serie "4x100 crol + 8x50 mariposa" tiene dos bloques.
    public class Bloque
    {
        public int IdBloque { get; set; }
        public string Descripcion { get; set; } // ej: "Crol a ritmo 200"
        public int Distancia { get; set; } // metros de CADA repeticion
        public int Repeticiones { get; set; }
        public int Orden { get; set; } // posicion del bloque dentro de la serie

        // Solo la columna de la clave foranea (la base la necesita). No es
        // una referencia al objeto TrabajoDiario, asi que se respeta la
        // regla de composicion del diagrama.
        public int IdTrabajo { get; set; }

        // Composicion: un resultado es de un unico bloque
        public List<Resultado> Resultados { get; set; } = new List<Resultado>();

        public bool TieneResultados()
        {
            return Resultados.Any();
        }
    }
}
