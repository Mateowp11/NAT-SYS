using System;
using System.Collections.Generic;
using System.Text;

namespace NatSys.Entidades
{
    public class Prueba
    {
        public int IdPrueba { get; set; }
        public string Nombre { get; set; }
        public int Distancia { get; set; } // metros

        public int IdEstilo { get; set; }
        public Estilo Estilo { get; set; }

        // Cuantos tramos tiene la prueba segun el largo de pileta
        public int GetNroTramos(int largoPileta)
        {
            return Distancia / largoPileta;
        }

        public decimal[] GetCoeficientesEstilo()
        {
            return Estilo?.GetCoeficientes();
        }
    }
}
