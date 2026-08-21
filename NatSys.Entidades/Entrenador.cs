using System;
using System.Collections.Generic;
using System.Text;

namespace NatSys.Entidades
{
    public class Entrenador : Persona
    {
        // Igual que en Atleta: no hay "IdEntrenador" propio, usa el
        // IdPersona heredado.
        public string Estado { get; set; }
        public string Especialidad { get; set; }

        public List<PlanPasaje> Planes { get; set; } = new List<PlanPasaje>();

        public bool EstaActivo()
        {
            return Estado == "activo";
        }
    }
}
