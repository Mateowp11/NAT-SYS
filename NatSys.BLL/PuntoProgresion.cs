// Proyecto: NatSys.BLL
//
// Una fila de la "progresion del record personal" del reporte de
// viabilidad: cada vez que el atleta mejoro su mejor marca, queda una fila.
// Todo en texto, listo para la grilla y el PDF.

namespace NatSys.BLL
{
    public class PuntoProgresion
    {
        public string Fecha { get; set; } = "";
        public string Tiempo { get; set; } = "";
        public string Tipo { get; set; } = "";   // oficial / test
        public string Mejora { get; set; } = ""; // cuanto bajo respecto del RP anterior, ej "-0.85 s"
    }
}
