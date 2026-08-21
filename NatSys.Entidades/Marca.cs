using System;
using System.Collections.Generic;
using System.Text;

namespace NatSys.Entidades
{
    public class Marca
    {
        public int IdMarca { get; set; }
        public string Tiempo { get; set; } // formato mm:ss.cc
        public string Tipo { get; set; } // "oficial" / "test"
        public bool EsRecordPersonal { get; set; }
        public int LargoPileta { get; set; }
        public DateTime Fecha { get; set; }

        public int IdAtleta { get; set; }
        public Atleta Atleta { get; set; }

        public int IdPrueba { get; set; }
        public Prueba Prueba { get; set; }

        public int? IdTorneo { get; set; } // nulo si es test de entrenamiento o SV
        public Torneo TorneoRegistro { get; set; }

        public bool EsOficial()
        {
            return Tipo == "oficial";
        }

        // "SV" = Sin Valor. Se usa en el marcador que devuelve
        // Atleta.ObtenerRP() cuando el atleta no tiene marcas previas en
        // esa prueba, para que la Vista muestre "SV" sin recibir null.
        public bool EsSV()
        {
            return Tipo == "SV";
        }

        public void MarcarComoRP()
        {
            EsRecordPersonal = true;
        }

        // Compara contra otra marca de la misma prueba (util para saber cual es mejor)
        public int CompararCon(Marca otra)
        {
            return string.Compare(Tiempo, otra.Tiempo, StringComparison.Ordinal);
        }
    }
}
