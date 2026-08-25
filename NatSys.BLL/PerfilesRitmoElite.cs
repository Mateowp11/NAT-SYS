// Proyecto: NatSys.BLL
//
// Coeficientes de ritmo (Inicial, Medio, Final) diferenciados por estilo
// Y por largo de pileta, basados en patrones documentados de natacion de
// nivel elite:
//
// - Piscina corta es sistematicamente mas rapida que piscina larga por
//   los virajes: cada viraje da un impulso de velocidad + una salida
//   submarina que "descansa" al nadador durante la carrera.
// - Esa ventaja NO es igual en los 4 estilos. Segun analisis de records
//   oficiales, el orden de mayor a menor diferencia piscina corta vs
//   larga es: Espalda, Pecho, Crol, Mariposa (la salida submarina rinde
//   mas ventaja relativa en espalda y pecho que en mariposa).
// - Esa ventaja se refleja sobre todo en el tramo MEDIO de la prueba (el
//   que mas se beneficia de virajes frecuentes), no tanto en el primer
//   ni el ultimo tramo.
//
// OJO: esto es un modelo que refleja esas diferencias documentadas, no
// un promedio literal de splits de nadadores puntuales de un mundial
// especifico - para eso harian falta datos oficiales de split completos
// que no estan disponibles con esa granularidad.

using System.Collections.Generic;

namespace NatSys.BLL
{
    public static class PerfilesRitmoElite
    {
        // Clave: (nombre del estilo en minusculas, largo de pileta)
        // Valor: { CoefFatigaInicial, CoefFatigaMedio, CoefFatigaFinal }
        private static readonly Dictionary<(string, int), decimal[]> _perfiles =
            new Dictionary<(string, int), decimal[]>
        {
            // Piscina larga (50m): menos virajes, mayor fatiga relativa
            // acumulada en el tramo medio
            { ("crol", 50),     new decimal[] { 0.97m, 1.05m, 1.00m } },
            { ("espalda", 50),  new decimal[] { 0.97m, 1.07m, 1.01m } },
            { ("pecho", 50),    new decimal[] { 0.96m, 1.09m, 1.02m } },
            { ("mariposa", 50), new decimal[] { 0.95m, 1.10m, 1.04m } },

            // Piscina corta (25m): el tramo medio se aplana por los
            // virajes extra. La magnitud del aplanamiento varia por
            // estilo, segun la diferencia real documentada.
            { ("crol", 25),     new decimal[] { 0.97m, 1.02m, 1.00m } },
            { ("espalda", 25),  new decimal[] { 0.97m, 0.99m, 1.01m } },
            { ("pecho", 25),    new decimal[] { 0.96m, 1.01m, 1.02m } },
            { ("mariposa", 25), new decimal[] { 0.95m, 1.07m, 1.03m } },
        };

        // Devuelve {Inicial, Medio, Final} para ese estilo+largo, o null
        // si no esta en la tabla (por ejemplo un largo de pileta no
        // estandar) - en ese caso MotorPasajes cae a otro resguardo.
        public static decimal[] ObtenerCoeficientes(string nombreEstilo, int largoPileta)
        {
            var clave = (nombreEstilo?.Trim().ToLowerInvariant(), largoPileta);
            return _perfiles.TryGetValue(clave, out var coeficientes) ? coeficientes : null;
        }
    }
}