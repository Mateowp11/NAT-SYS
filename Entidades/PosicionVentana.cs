// Proyecto: NatSys.UI
//
// Tamaño y lugar en que el usuario dejo una ventana. Lo guarda y lo lee
// AjustesVentana en el archivo ventanas.json.

namespace NatSys.UI
{
    internal class PosicionVentana
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Ancho { get; set; }
        public int Alto { get; set; }
        public bool Maximizada { get; set; }
    }
}
