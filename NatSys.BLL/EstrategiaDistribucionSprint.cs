// Proyecto: NatSys.BLL
//
// Estrategia para pruebas de VELOCIDAD PURA (50m y 100m). La evidencia
// (analisis de Mundiales de pileta corta, Frontiers 2023) muestra que en
// estas pruebas el nadador sale al maximo desde la salida y el ritmo
// decae de forma CONTINUA durante toda la carrera - a diferencia de
// media distancia y fondo, aca NO hay recuperacion de velocidad en el
// cierre. Por eso esta estrategia no usa el coeficiente Final como
// "mejora": simplemente extiende la fatiga en linea recta.

using System.Collections.Generic;

namespace NatSys.BLL
{
    public class EstrategiaDistribucionSprint : IEstrategiaDistribucion
    {
        public List<decimal> CalcularPesos(int nroTramos, decimal[] coeficientes)
        {
            // coeficientes[0] = Inicial (el tramo mas rapido, arranque)
            // coeficientes[1] = Medio (referencia de cuanto fatiga por tramo)
            decimal inicial = coeficientes[0];
            decimal incremento = nroTramos > 1
                ? (coeficientes[1] - coeficientes[0]) / nroTramos
                : 0;

            var pesos = new List<decimal>();
            for (int i = 0; i < nroTramos; i++)
            {
                pesos.Add(inicial + incremento * i);
            }

            return pesos;
        }
    }
}