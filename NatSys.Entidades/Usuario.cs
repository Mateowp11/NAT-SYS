using System;
using System.Collections.Generic;
using System.Text;

namespace NatSys.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string Estado { get; set; }
        public int IntentosFallidos { get; set; }
        public DateTime? UltimoAcceso { get; set; }

        // Para recuperacion de contraseña (CU-SEG-05). La respuesta se guarda
        // hasheada igual que la clave: nunca en texto plano.
        public string PreguntaSeguridad { get; set; }
        public string RespuestaSeguridadHash { get; set; }

        public int IdPersona { get; set; }
        public Persona Persona { get; set; }

        public List<Grupo> Grupos { get; set; } = new List<Grupo>();
    }
}
