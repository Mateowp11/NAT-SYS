using System;
using System.Collections.Generic;
using System.Text;

namespace NatSys.Entidades
{
    public class Estilo
    {
        public int IdEstilo { get; set; }
        public string Nombre { get; set; } // Crol, Espalda, Pecho, Mariposa
        public decimal CoefFatigaInicial { get; set; }
        public decimal CoefFatigaMedio { get; set; }
        public decimal CoefFatigaFinal { get; set; }

        // Devuelve los 3 coeficientes juntos, para que el MotorPasajes
        // distribuya el esfuerzo por tramo segun el estilo
        public decimal[] GetCoeficientes()
        {
            return new[] { CoefFatigaInicial, CoefFatigaMedio, CoefFatigaFinal };
        }
    }
}
