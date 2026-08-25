// Proyecto: NatSys.BLL
//
// Datos para el calculo de pruebas COMBINADAS (200 y 400 Combinado):
// - El orden de estilos es fijo, definido por el reglamento de
//   competencia (World Aquatics), no es configurable.
// - El porcentaje del tiempo total que insume cada pierna NO es 25%
//   parejo: se basa en analisis de splits de 200 Combinado de nivel
//   elite. Pecho es sistematicamente la pierna mas lenta (llega despues
//   de mariposa y espalda ya con fatiga acumulada, y es la tecnica mas
//   costosa); Mariposa la mas rapida (primera pierna, sin desgaste
//   previo).
//
// Se usa el mismo reparto para 200 y 400 Combinado: la jerarquia de
// velocidad relativa entre estilos (pecho mas lento, mariposa mas
// rapido) es una caracteristica estable de cada tecnica, no depende
// tanto de la distancia total.

using System.Collections.Generic;

namespace NatSys.BLL
{
    public static class PerfilesCombinado
    {
        public static readonly string[] OrdenEstilos = { "mariposa", "espalda", "pecho", "crol" };

        // Porcentajes del tiempo total (suman 100%)
        public static readonly Dictionary<string, decimal> PorcentajePorPierna = new Dictionary<string, decimal>
        {
            { "mariposa", 0.22m },
            { "espalda", 0.25m },
            { "pecho", 0.29m },
            { "crol", 0.24m }
        };
    }
}