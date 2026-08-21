// Proyecto: NatSys.BLL / Seguridad

using System;

namespace NatSys.BLL
{
    public class CredencialesInvalidasException : Exception
    {
        public CredencialesInvalidasException(string mensaje) : base(mensaje) { }
    }
}