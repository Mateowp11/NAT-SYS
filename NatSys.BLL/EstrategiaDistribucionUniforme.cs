// Proyecto: NatSys.BLL
//
// Estrategia de resguardo: reparte el tiempo en partes iguales entre
// todos los tramos, sin importar el estilo. MotorPasajes cae aca si el
// estilo de la prueba no tiene sus 3 coeficientes cargados, para no
// romper el calculo por datos incompletos.

using System.Collections.Generic;

namespace NatSys.BLL
{
    public class EstrategiaDistribucionUniforme : IEstrategiaDistribucion
    {
        public List<decimal> CalcularPesos(int nroTramos, decimal[] coeficientes)
        {
            var pesos = new List<decimal>();
            for (int i = 0; i < nroTramos; i++)
                pesos.Add(1m);
            return pesos;
        }
    }
}