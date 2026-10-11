// Proyecto: NatSys.BLL
//
// Una fila del resumen del reporte de evolucion: como le fue al atleta
// en UNA prueba durante el periodo. Todo en texto, listo para la grilla
// y el PDF (los nombres de las propiedades son los titulos de columna).

namespace NatSys.BLL
{
    public class ResumenEvolucionPrueba
    {
        public string Prueba { get; set; } = "";
        public int Marcas { get; set; }
        public string Primera { get; set; } = "";
        public string Mejor { get; set; } = "";
        public string Ultima { get; set; } = "";
        public string Mejora { get; set; } = "";  // ej "2.15 %"
        public string Estado { get; set; } = "";  // Mejora notable / Mejora / Estancado / En retroceso
    }
}
