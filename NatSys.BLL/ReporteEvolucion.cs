// Proyecto: NatSys.BLL
//
// Resultado del reporte RF-06.2 (Analisis de evolucion por estilo): las
// lineas del grafico y la tabla resumen por prueba.

using System;
using System.Collections.Generic;

namespace NatSys.BLL
{
    public class ReporteEvolucion
    {
        public int LargoPileta { get; set; }
        public string Estilo { get; set; } = "";  // "Todos" o el estilo filtrado
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }

        // Solo pruebas con 2 marcas o mas (con una sola no hay evolucion)
        public List<SerieEvolucion> Series { get; set; } = new List<SerieEvolucion>();

        // Todas las pruebas con marcas en el periodo
        public List<ResumenEvolucionPrueba> Resumen { get; set; } = new List<ResumenEvolucionPrueba>();
    }
}
