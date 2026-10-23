// Proyecto: NatSys.Entidades
// Iteracion 2 - RF-09 / CU-08 Cargar resultados de la sesion (nucleo)

using System;
using System.Collections.Generic;
using System.Linq;

namespace NatSys.Entidades
{
    // Lo que un atleta cargo sobre UN bloque. Regla de negocio: hay un unico
    // resultado por atleta y por bloque (si vuelve a cargar, se corrige el
    // existente - CU-08-EXT-01). Lo asegura el indice unico del DbContext.
    public class Resultado
    {
        public const string EstadoCompleto = "Completo"; // cargo todas las repeticiones
        public const string EstadoParcial = "Parcial";   // dejo alguna sin cargar

        public int IdResultado { get; set; }
        public DateTime FechaCarga { get; set; }
        public string Estado { get; set; } // EstadoCompleto / EstadoParcial

        // Solo la columna de la clave foranea hacia Bloque (igual que en
        // Bloque.IdTrabajo). Sirve para saber en pantalla a que bloque
        // corresponde cada resultado al precargar los tiempos.
        public int IdBloque { get; set; }

        public int IdAtleta { get; set; }
        public Atleta Atleta { get; set; }

        // Composicion: los tiempos no existen sin su resultado
        public List<TiempoRepeticion> Tiempos { get; set; } = new List<TiempoRepeticion>();

        // Constructor sin parametros: lo usa EF Core cuando lee de la base.
        // Es privado para que el resto del codigo use el del diagrama.
        private Resultado()
        {
        }

        public Resultado(Atleta atleta, string estado)
        {
            Atleta = atleta;
            IdAtleta = atleta.IdPersona;
            Estado = estado;
            FechaCarga = DateTime.Now;
        }

        // Reemplaza los tiempos cargados (se usa tanto en la primera carga
        // como al corregir) y actualiza el estado y la fecha de carga.
        public void SetTiempos(List<TiempoRepeticion> tiempos, string estado)
        {
            Tiempos.Clear();
            Tiempos.AddRange(tiempos);
            Estado = estado;
            FechaCarga = DateTime.Now;
        }

        public bool EstaCompleto()
        {
            return Estado == EstadoCompleto;
        }
    }
}
