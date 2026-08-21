using System;
using System.Collections.Generic;
using System.Text;

namespace NatSys.Entidades
{
    public class Torneo
    {
        public int IdTorneo { get; set; }
        public string NombreTorneo { get; set; }
        public DateTime Fecha { get; set; }
        public string Sede { get; set; }
        public int LargoPileta { get; set; }
        public string Estado { get; set; } // "activo" / "finalizado"

        public List<Marca> Marcas { get; set; } = new List<Marca>();

        public bool TieneMarcasAsociadas()
        {
            return Marcas.Any();
        }

        public void Finalizar()
        {
            Estado = "finalizado";
        }
    }
}
