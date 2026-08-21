using System;
using System.Collections.Generic;
using System.Text;

namespace NatSys.Entidades
{
    public class Grupo
    {
        public int IdGrupo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        public List<Permiso> Permisos { get; set; } = new List<Permiso>();
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
