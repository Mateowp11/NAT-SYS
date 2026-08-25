// Proyecto: NatSys.BLL
//
// PATRON STRATEGY (de comportamiento): MotorPasajes no sabe COMO se
// reparte el tiempo entre tramos, solo que existe una estrategia capaz
// de hacerlo. Las estrategias concretas son intercambiables en runtime.

using System.Collections.Generic;

namespace NatSys.BLL
{
    public interface IEstrategiaDistribucion
    {
        // Devuelve un peso relativo por cada tramo (mismo orden, 1..N).
        // MotorPasajes normaliza estos pesos despues para que sumen el
        // tiempo total exacto - no hace falta que sumen 1 aca.
        List<decimal> CalcularPesos(int nroTramos, decimal[] coeficientes);
    }
}