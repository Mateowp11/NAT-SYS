using System;
using System.Collections.Generic;
using System.Text;

namespace NatSys.Entidades
{
    public class PlanPasaje
    {
        public int IdPlanPasaje { get; set; }
        public string MarcaObjetivo { get; set; } // formato mm:ss.cc
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } // "Disponible" / "Reemplazado"
        public int LargoPileta { get; set; }

        public int IdAtleta { get; set; }
        public Atleta Atleta { get; set; }

        public int IdPrueba { get; set; }
        public Prueba Prueba { get; set; }

        public int IdEntrenador { get; set; }
        public Entrenador Entrenador { get; set; }

        public List<Pasaje> Pasajes { get; set; } = new List<Pasaje>();

        // Marca este plan como reemplazado (cuando se genera uno nuevo para
        // el mismo atleta/prueba, segun vimos en tu diagrama de secuencias CU-01)
        public void MarcarComoReemplazado()
        {
            Estado = "Reemplazado";
        }
    }
}
