using System;
using System.Collections.Generic;
using System.Text;

namespace NatSys.Entidades
{
    public class Sesion
    {
        public int IdSesion { get; set; }

        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public DateTime Inicio { get; set; }
        public DateTime? Fin { get; set; }
        public string Estado { get; set; }
    }
}
