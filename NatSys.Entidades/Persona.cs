// Proyecto: NatSys.Entidades

using System;

namespace NatSys.Entidades
{
    // Abstracta: en tu diagrama "Persona" esta en italica, que en UML
    // significa clase abstracta. Nunca se crea una Persona "pelada",
    // siempre es un Atleta o un Entrenador.
    public abstract class Persona
    {
        // Tu diagrama no dibuja un Id en la caja de Persona, pero lo
        // necesitamos: es lo unico que permite que Usuario tenga UNA
        // relacion generica a "persona: Persona" que funcione tanto para
        // un Atleta como para un Entrenador.
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        // Opcionales: tu RF-02 solo exige Nombre, Apellido y
        // FechaNacimiento como obligatorios para un Atleta. El "?" le dice
        // a C# y a EF Core que estos campos pueden quedar sin valor.
        public string? Email { get; set; }
        public string? Telefono { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string GetNombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}