// Proyecto: NatSys.Entidades
// Iteracion 2 - RF-07 / CU-06 Gestionar grupos de entrenamiento

using System;
using System.Collections.Generic;
using System.Linq;

namespace NatSys.Entidades
{
    // Conjunto de atletas que entrenan juntos en un mismo horario, con un
    // entrenador a cargo. No confundir con Grupo, que es el grupo de
    // SEGURIDAD (el que agrupa permisos de usuarios).
    public class GrupoEntrenamiento
    {
        public int IdGrupoEntrenamiento { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; } // "activo" / "inactivo"

        // Entrenador responsable (relacion bidireccional: Entrenador
        // tambien tiene la lista de sus grupos)
        public int IdEntrenador { get; set; }
        public Entrenador Entrenador { get; set; }

        // Agregacion muchos a muchos: un atleta con doble turno esta en dos
        // grupos, y si se borra el grupo los atletas siguen existiendo.
        public List<Atleta> Atletas { get; set; } = new List<Atleta>();

        public void AgregarAtleta(Atleta atleta)
        {
            if (!Atletas.Any(a => a.IdPersona == atleta.IdPersona))
                Atletas.Add(atleta);
        }

        public void QuitarAtleta(Atleta atleta)
        {
            var existente = Atletas.FirstOrDefault(a => a.IdPersona == atleta.IdPersona);
            if (existente != null)
                Atletas.Remove(existente);
        }
    }
}
