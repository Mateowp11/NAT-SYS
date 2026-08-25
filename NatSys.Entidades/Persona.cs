using System;
using System.Collections.Generic;
using System.Text;

namespace NatSys.Entidades
{
    public abstract class Persona
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public string GetNombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}
