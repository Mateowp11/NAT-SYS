// Proyecto: NatSys.BLL / Seguridad
//
// Separada de CredencialesInvalidasException para que la Vista pueda
// distinguir este caso puntual (por ejemplo, para ofrecer el boton de
// "Recuperar contraseña" solo cuando la cuenta esta bloqueada).

using System;

namespace NatSys.BLL
{
    public class CuentaBloqueadaException : Exception
    {
        public CuentaBloqueadaException(string mensaje) : base(mensaje) { }
    }
}