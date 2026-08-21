using System;
using System.Collections.Generic;
using System.Text;

namespace NatSys.Entidades
{
    public class Atleta : Persona
    {
        // OJO: no declaramos "IdAtleta" propio. Al heredar de Persona, el
        // Id de un Atleta ES el mismo IdPersona (asi funciona la herencia
        // en una base relacional: no hay dos numeros de identidad distintos
        // para la misma fila, uno hace de "raiz" y el resto lo hereda).
        public string Categoria { get; set; }
        public string Estado { get; set; } // "activo" / "inactivo"

        public List<Prueba> Pruebas { get; set; } = new List<Prueba>();
        public List<Marca> Marcas { get; set; } = new List<Marca>();

        public bool EstaActivo()
        {
            return Estado == "activo";
        }

        public void Desactivar()
        {
            Estado = "inactivo";
        }

        // Devuelve el record personal del atleta para una prueba especifica.
        // Si no tiene marcas previas, devuelve un marcador "SV" en vez de
        // null (patron Null Object): asi Marca.EsSV() le dice a la Vista
        // que mostrar sin que cada pantalla tenga que chequear null a mano.
        public Marca ObtenerRP(int idPrueba)
        {
            var mejorMarca = Marcas
                .Where(m => m.IdPrueba == idPrueba && m.EsRecordPersonal)
                .FirstOrDefault();

            return mejorMarca ?? new Marca { Tipo = "SV", IdAtleta = IdPersona, IdPrueba = idPrueba };
        }


    }
}
