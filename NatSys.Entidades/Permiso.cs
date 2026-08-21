using System;
using System.Collections.Generic;
using System.Text;

namespace NatSys.Entidades
{
    public class Permiso
    {
        public int IdPermiso { get; set; }
        public string Nombre { get; set; }
        public string Modulo { get; set; }
        public string Accion { get; set; }

        public List<Grupo> Grupos { get; set; } = new List<Grupo>();
    }
}
