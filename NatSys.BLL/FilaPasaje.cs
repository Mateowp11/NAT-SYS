// Proyecto: NatSys.BLL
//
// Una fila de la tabla de pasajes, ya lista para mostrar en pantalla.
// La arma FachadaPasajes, asi la pantalla no tiene que hacer cuentas:
// solo la muestra en la grilla (el DataGridView usa los nombres de las
// propiedades como titulos de columna).

namespace NatSys.BLL
{
    public class FilaPasaje
    {
        public int Tramo { get; set; }
        public string Metros { get; set; } = "";    // metros acumulados, ej "100 m"
        public string Parcial { get; set; } = "";   // tiempo de ese tramo, ej "32.77"
        public string Acumulado { get; set; } = ""; // tiempo desde la salida, ej "01:02.93"
    }
}