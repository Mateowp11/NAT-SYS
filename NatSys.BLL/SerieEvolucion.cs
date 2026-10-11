// Proyecto: NatSys.BLL
//
// Los puntos de UNA linea del grafico de evolucion: una prueba, sus
// fechas y el % de mejora de cada marca respecto de la primera del
// periodo. Usamos arrays porque es lo que recibe ScottPlot.

using System;

namespace NatSys.BLL
{
    public class SerieEvolucion
    {
        public string Prueba { get; set; } = "";
        public DateTime[] Fechas { get; set; } = Array.Empty<DateTime>();
        public double[] PorcentajesMejora { get; set; } = Array.Empty<double>();
    }
}
