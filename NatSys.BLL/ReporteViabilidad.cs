// Proyecto: NatSys.BLL
//
// Resultado del reporte RD-06.1 (Viabilidad de marca). Lo arma
// GestorReportes y lo muestran la pantalla y el PDF.

using System;
using System.Collections.Generic;

namespace NatSys.BLL
{
    public class ReporteViabilidad
    {
        // ---- Datos de la consulta ----
        public string Prueba { get; set; } = "";
        public int LargoPileta { get; set; }
        public string MarcaObjetivo { get; set; } = "";
        public DateTime FechaObjetivo { get; set; }

        // ---- Historial procesado ----
        public int CantidadMarcas { get; set; }
        public string RecordPersonal { get; set; } = "";
        public DateTime FechaRecordPersonal { get; set; }
        public List<PuntoProgresion> Progresion { get; set; } = new List<PuntoProgresion>();

        // ---- Tendencia (regresion lineal de los ultimos 12 meses) ----
        public bool TieneTendencia { get; set; }

        // Segundos que mejora por mes (positivo = mejora, negativo = empeora)
        public decimal MejoraPorMes { get; set; }

        // Tiempo que tendria en la fecha objetivo si sigue a este ritmo
        public string Proyeccion { get; set; } = "";

        // ---- Resultado ----
        public string Veredicto { get; set; } = "";   // Alcanzable / Ambiciosa / Poco realista
        public string Conclusion { get; set; } = "";  // explicacion en una o dos oraciones
    }
}
