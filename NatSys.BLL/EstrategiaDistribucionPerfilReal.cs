// Proyecto: NatSys.BLL
//
// Estrategia PRINCIPAL del motor (patron Strategy): reparte la marca
// objetivo segun como nadaron de verdad los finalistas de un Mundial o de
// los Juegos Olimpicos en esa misma prueba (ver PerfilesRitmoElite).
//
// Aca "coeficientes" ya trae un porcentaje por tramo (ej: 200 Crol en
// pileta de 50 = 23.36 / 25.38 / 25.68 / 25.58), asi que los pesos son
// directamente esos porcentajes. MotorPasajes despues los convierte en
// segundos sobre la marca objetivo.

using System;
using System.Collections.Generic;

namespace NatSys.BLL
{
    public class EstrategiaDistribucionPerfilReal : IEstrategiaDistribucion
    {
        public List<decimal> CalcularPesos(int nroTramos, decimal[] coeficientes)
        {
            // Control de seguridad: tiene que haber un porcentaje por tramo
            if (coeficientes == null || coeficientes.Length != nroTramos)
                throw new ArgumentException("El perfil de ritmo no coincide con la cantidad de tramos de la prueba.");

            return new List<decimal>(coeficientes);
        }
    }
}