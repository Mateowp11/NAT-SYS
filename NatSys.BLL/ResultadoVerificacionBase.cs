// Proyecto: NatSys.BLL
//
// Lo que devuelve GestorBaseDatos.Verificar(): si se puede usar la base
// y, si no, que mensaje mostrarle al usuario.

namespace NatSys.BLL
{
    public class ResultadoVerificacionBase
    {
        public bool Correcto { get; set; }

        // true cuando el problema puede resolverse solo (internet, base
        // despertandose) y tiene sentido ofrecer "Reintentar"
        public bool SePuedeReintentar { get; set; }

        public string Mensaje { get; set; } = "";

        public static ResultadoVerificacionBase Ok() =>
            new ResultadoVerificacionBase { Correcto = true };

        public static ResultadoVerificacionBase Error(string mensaje, bool sePuedeReintentar) =>
            new ResultadoVerificacionBase { Correcto = false, Mensaje = mensaje, SePuedeReintentar = sePuedeReintentar };
    }
}