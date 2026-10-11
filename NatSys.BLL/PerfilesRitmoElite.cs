// Proyecto: NatSys.BLL
//
// Perfiles de ritmo REALES: que porcentaje del tiempo total nada un
// nadador de elite en cada tramo de la prueba.
//
// De donde salen los numeros:
// - Parciales oficiales (cronometraje Omega) de los 8 finalistas de cada
//   prueba, mujeres y varones: 16 nadadores por prueba.
// - Pileta larga: finales de los Juegos Olimpicos Paris 2024 y, para las
//   pruebas cuyos parciales de Paris no estaban publicados, finales del
//   Mundial de Singapur 2025.
// - Pileta corta: finales del Mundial de Piscina Corta Budapest 2024
//   (parciales cada 25 m).
//
// Como se calcularon: para cada nadador, cada parcial se dividio por su
// tiempo final (ej: 30.20 / 129.12 = 23.39 %). Despues se promediaron los
// 16 nadadores y se redondeo a 2 decimales (cada fila suma 100).
// Usamos porcentajes y no segundos porque asi el perfil sirve para
// cualquier nivel: el motor los aplica sobre la marca objetivo.
//
// Se promedian mujeres y varones juntos: la diferencia entre ambos fue
// chica (menos de 0.8 puntos en el peor tramo) y el sistema no guarda el
// sexo del atleta.
//
// Las pruebas de 50 m en pileta de 50 no aparecen: tienen un solo tramo.

using System.Collections.Generic;

namespace NatSys.BLL
{
    public static class PerfilesRitmoElite
    {
        // Clave: (estilo en minusculas, distancia, largo de pileta)
        // Valor: porcentaje del tiempo total de cada tramo, en orden
        private static readonly Dictionary<(string, int, int), decimal[]> _perfiles =
            new Dictionary<(string, int, int), decimal[]>
        {
            // ===== PILETA LARGA (50 m): un porcentaje por cada 50 m =====
            // 100 Crol - Mundial Singapur 2025 (16 finalistas)
            { ("crol", 100, 50), new decimal[] { 47.86m, 52.14m } },
            // 200 Crol - JJOO Paris 2024 (16 finalistas)
            { ("crol", 200, 50), new decimal[] { 23.36m, 25.38m, 25.68m, 25.58m } },
            // 400 Crol - JJOO Paris 2024 (16 finalistas)
            { ("crol", 400, 50), new decimal[] { 11.45m, 12.39m, 12.60m, 12.71m, 12.73m, 12.82m, 12.77m, 12.53m } },
            // 800 Crol - JJOO Paris 2024 + Mundial Singapur 2025 (16 finalistas)
            { ("crol", 800, 50), new decimal[] {
                5.72m, 6.17m, 6.21m, 6.28m, 6.29m, 6.32m, 6.29m, 6.34m, 6.32m, 6.34m,
                6.32m, 6.35m, 6.35m, 6.35m, 6.32m, 6.03m
            } },
            // 1500 Crol - Mundial Singapur 2025 (16 finalistas)
            { ("crol", 1500, 50), new decimal[] {
                3.04m, 3.28m, 3.31m, 3.33m, 3.32m, 3.34m, 3.34m, 3.34m, 3.34m, 3.34m,
                3.34m, 3.35m, 3.34m, 3.35m, 3.35m, 3.35m, 3.35m, 3.35m, 3.35m, 3.37m,
                3.36m, 3.38m, 3.37m, 3.38m, 3.37m, 3.38m, 3.36m, 3.37m, 3.34m, 3.21m
            } },
            // 100 Espalda - JJOO Paris 2024 (16 finalistas)
            { ("espalda", 100, 50), new decimal[] { 48.25m, 51.75m } },
            // 200 Espalda - Mundial Singapur 2025 (16 finalistas)
            { ("espalda", 200, 50), new decimal[] { 23.38m, 25.27m, 25.61m, 25.74m } },
            // 100 Pecho - JJOO Paris 2024 (16 finalistas)
            { ("pecho", 100, 50), new decimal[] { 46.44m, 53.56m } },
            // 200 Pecho - Mundial Singapur 2025 (15 finalistas)
            { ("pecho", 200, 50), new decimal[] { 22.52m, 25.30m, 25.73m, 26.45m } },
            // 100 Mariposa - JJOO Paris 2024 + Mundial Singapur 2025 (16 finalistas)
            { ("mariposa", 100, 50), new decimal[] { 46.30m, 53.70m } },
            // 200 Mariposa - Mundial Singapur 2025 (16 finalistas)
            { ("mariposa", 200, 50), new decimal[] { 22.14m, 25.33m, 25.87m, 26.66m } },
            // 200 Combinado - Mundial Singapur 2025 (16 finalistas)
            { ("combinado", 200, 50), new decimal[] { 21.32m, 25.37m, 29.20m, 24.11m } },
            // 400 Combinado - JJOO Paris 2024 (16 finalistas)
            { ("combinado", 400, 50), new decimal[] { 10.45m, 11.99m, 12.92m, 12.67m, 14.19m, 14.51m, 11.84m, 11.43m } },

            // ===== PILETA CORTA (25 m): un porcentaje por cada 25 m =====
            // 50 Crol - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("crol", 50, 25), new decimal[] { 48.16m, 51.84m } },
            // 100 Crol - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("crol", 100, 25), new decimal[] { 22.76m, 25.11m, 26.06m, 26.07m } },
            // 200 Crol - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("crol", 200, 25), new decimal[] { 11.08m, 12.28m, 12.55m, 12.66m, 12.73m, 12.87m, 13.05m, 12.78m } },
            // 400 Crol - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("crol", 400, 25), new decimal[] {
                5.41m, 6.07m, 6.21m, 6.26m, 6.33m, 6.36m, 6.35m, 6.37m, 6.34m, 6.36m,
                6.35m, 6.36m, 6.38m, 6.37m, 6.37m, 6.11m
            } },
            // 800 Crol - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("crol", 800, 25), new decimal[] {
                2.73m, 3.02m, 3.09m, 3.10m, 3.13m, 3.13m, 3.15m, 3.14m, 3.15m, 3.15m,
                3.17m, 3.15m, 3.16m, 3.15m, 3.18m, 3.16m, 3.17m, 3.15m, 3.16m, 3.16m,
                3.16m, 3.16m, 3.16m, 3.15m, 3.15m, 3.15m, 3.17m, 3.15m, 3.15m, 3.14m,
                3.11m, 2.95m
            } },
            // 1500 Crol - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("crol", 1500, 25), new decimal[] {
                1.46m, 1.61m, 1.64m, 1.65m, 1.66m, 1.66m, 1.67m, 1.66m, 1.67m, 1.67m,
                1.67m, 1.67m, 1.67m, 1.67m, 1.68m, 1.66m, 1.68m, 1.66m, 1.67m, 1.67m,
                1.67m, 1.67m, 1.67m, 1.67m, 1.68m, 1.67m, 1.68m, 1.67m, 1.68m, 1.67m,
                1.68m, 1.67m, 1.68m, 1.67m, 1.68m, 1.67m, 1.68m, 1.68m, 1.69m, 1.68m,
                1.68m, 1.68m, 1.69m, 1.68m, 1.68m, 1.68m, 1.69m, 1.68m, 1.68m, 1.68m,
                1.68m, 1.68m, 1.68m, 1.68m, 1.68m, 1.68m, 1.67m, 1.67m, 1.64m, 1.56m
            } },
            // 50 Espalda - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("espalda", 50, 25), new decimal[] { 49.17m, 50.83m } },
            // 100 Espalda - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("espalda", 100, 25), new decimal[] { 23.33m, 24.87m, 25.87m, 25.93m } },
            // 200 Espalda - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("espalda", 200, 25), new decimal[] { 11.21m, 12.23m, 12.64m, 12.78m, 12.81m, 12.89m, 12.89m, 12.55m } },
            // 50 Pecho - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("pecho", 50, 25), new decimal[] { 45.40m, 54.60m } },
            // 100 Pecho - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("pecho", 100, 25), new decimal[] { 21.35m, 25.49m, 26.19m, 26.97m } },
            // 200 Pecho - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("pecho", 200, 25), new decimal[] { 10.30m, 12.29m, 12.57m, 12.74m, 12.85m, 12.99m, 13.08m, 13.18m } },
            // 50 Mariposa - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("mariposa", 50, 25), new decimal[] { 45.55m, 54.45m } },
            // 100 Mariposa - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("mariposa", 100, 25), new decimal[] { 21.29m, 25.11m, 26.31m, 27.29m } },
            // 200 Mariposa - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("mariposa", 200, 25), new decimal[] { 10.18m, 12.25m, 12.50m, 12.72m, 12.88m, 12.95m, 13.16m, 13.36m } },
            // 100 Combinado - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("combinado", 100, 25), new decimal[] { 20.44m, 25.06m, 29.47m, 25.03m } },
            // 200 Combinado - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("combinado", 200, 25), new decimal[] { 9.78m, 11.83m, 12.90m, 12.21m, 14.34m, 14.72m, 12.65m, 11.57m } },
            // 400 Combinado - Mundial P. Corta Budapest 2024 (16 finalistas)
            { ("combinado", 400, 25), new decimal[] {
                4.79m, 5.79m, 5.99m, 6.11m, 6.53m, 6.28m, 6.37m, 6.29m, 6.99m, 7.09m,
                7.16m, 7.22m, 6.14m, 5.83m, 5.83m, 5.59m
            } },
        };

        // Devuelve los porcentajes de esa prueba, o null si no hay datos
        // reales para esa combinacion (ahi MotorPasajes usa el modelo
        // teorico con los coeficientes del Estilo).
        public static decimal[]? ObtenerPerfil(string nombreEstilo, int distancia, int largoPileta)
        {
            if (string.IsNullOrWhiteSpace(nombreEstilo)) return null;

            var clave = (nombreEstilo.Trim().ToLowerInvariant(), distancia, largoPileta);
            return _perfiles.TryGetValue(clave, out var perfil) ? perfil : null;
        }
    }
}