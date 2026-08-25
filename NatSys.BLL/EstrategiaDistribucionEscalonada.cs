// Proyecto: NatSys.BLL
//
// Estrategia "real" del negocio: el primer tramo usa el coeficiente
// Inicial, el ultimo el Final, y todos los del medio el coeficiente
// Medio. Asi se refleja que el nadador arranca fuerte, sostiene el ritmo,
// y aprieta en el cierre - con una curva distinta segun el estilo
// (Crol, Espalda, Pecho y Mariposa tienen sus propios 3 coeficientes).

using System.Collections.Generic;

namespace NatSys.BLL
{
    public class EstrategiaDistribucionEscalonada : IEstrategiaDistribucion
    {
        public List<decimal> CalcularPesos(int nroTramos, decimal[] coeficientes)
        {
            // coeficientes[0] = Inicial, [1] = Medio, [2] = Final
            var pesos = new List<decimal>();

            for (int i = 0; i < nroTramos; i++)
            {
                if (i == 0)
                    pesos.Add(coeficientes[0]);
                else if (i == nroTramos - 1)
                    pesos.Add(coeficientes[2]);
                else
                    pesos.Add(coeficientes[1]);
            }

            return pesos;
        }
    }
}