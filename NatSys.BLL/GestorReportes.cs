// Proyecto: NatSys.BLL
//
// RF-06 (Informes de rendimiento). Los dos reportes PROCESAN el historial
// de marcas (no son simples consultas):
//
// - RD-06.1 Viabilidad de marca: arma la progresion del record personal,
//   calcula la tendencia del atleta con una regresion lineal, proyecta su
//   tiempo a la fecha objetivo y dice si la meta es alcanzable, ambiciosa
//   o poco realista.
// - RF-06.2 Evolucion por estilo: para cada prueba calcula el % de mejora
//   respecto de la primera marca del periodo y clasifica al atleta
//   (mejora notable / mejora / estancado / en retroceso). Incluye los
//   datos para el grafico.
//
// La BLL solo calcula y devuelve los resultados. Mostrarlos (pantalla,
// grafico o PDF) es tarea de la UI.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NatSys.DAL;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class GestorReportes
    {
        private static GestorReportes? _instancia;
        private readonly DALMarcas _marcaDAL;

        // Mismo umbral que usa MotorPasajes.EvaluarViabilidad (FA-01 de RF-01)
        private const decimal UmbralPocoRealista = 0.03m;

        // Con pocas marcas la recta puede dar mejoras irreales (ej: 5 s por
        // mes). Por eso la mejora mensual se limita al 1 % del RP.
        private const decimal MejoraMensualMaxima = 0.01m;

        private const decimal DiasPorMes = 30.44m;

        private GestorReportes(string connectionString)
        {
            _marcaDAL = new DALMarcas(connectionString);
        }

        public static void Inicializar(string connectionString)
        {
            if (_instancia == null)
                _instancia = new GestorReportes(connectionString);
        }

        public static GestorReportes GetInstancia()
        {
            if (_instancia == null)
                throw new InvalidOperationException("GestorReportes no fue inicializado.");
            return _instancia;
        }

        // ================== RD-06.1 Viabilidad de marca ==================

        public ReporteViabilidad GenerarReporteViabilidad(
            int idAtleta, Prueba prueba, int largoPileta, string marcaObjetivo, DateTime fechaObjetivo)
        {
            var motor = MotorPasajes.GetInstancia();

            if (!motor.ValidarFormatoTiempo(marcaObjetivo))
                throw new ArgumentException(MotorPasajes.MensajeFormatoTiempo);

            var marcas = _marcaDAL.ObtenerMarcasDePrueba(idAtleta, prueba.IdPrueba, largoPileta)
                .OrderBy(m => m.Fecha)
                .ToList();

            // Pre-condicion de RF-06: tiene que haber marcas para analizar
            if (marcas.Count == 0)
                throw new InvalidOperationException("El atleta no tiene marcas registradas en esa prueba y pileta.");

            decimal objetivo = motor.ConvertirASegundos(marcaObjetivo);

            var reporte = new ReporteViabilidad
            {
                Prueba = prueba.Nombre,
                LargoPileta = largoPileta,
                MarcaObjetivo = FormatearSegundos(objetivo),
                FechaObjetivo = fechaObjetivo.Date,
                CantidadMarcas = marcas.Count
            };

            // 1) Progresion del RP: recorremos las marcas en orden de fecha
            //    y guardamos cada una que mejoro la mejor marca hasta ese momento
            decimal? mejorHastaAhora = null;
            Marca? marcaRP = null;

            foreach (var marca in marcas)
            {
                decimal segundos = motor.ConvertirASegundos(marca.Tiempo);
                if (mejorHastaAhora == null || segundos < mejorHastaAhora)
                {
                    reporte.Progresion.Add(new PuntoProgresion
                    {
                        Fecha = marca.Fecha.ToString("dd/MM/yyyy"),
                        Tiempo = motor.FormatearParaMostrar(marca.Tiempo),
                        Tipo = marca.Tipo,
                        Mejora = mejorHastaAhora == null
                            ? "Primera marca"
                            : "-" + FormatearDecimal(mejorHastaAhora.Value - segundos) + " s"
                    });
                    mejorHastaAhora = segundos;
                    marcaRP = marca;
                }
            }

            decimal rp = mejorHastaAhora!.Value;
            reporte.RecordPersonal = FormatearSegundos(rp);
            reporte.FechaRecordPersonal = marcaRP!.Fecha;

            // 2) Tendencia: cuantos segundos mejora por mes (regresion lineal)
            decimal mejoraPorMes = 0;
            decimal? pendiente = CalcularPendienteSegundosPorDia(marcas);
            if (pendiente != null)
            {
                reporte.TieneTendencia = true;
                mejoraPorMes = -pendiente.Value * DiasPorMes; // pendiente negativa = tiempos que bajan = mejora

                decimal tope = rp * MejoraMensualMaxima;
                if (mejoraPorMes > tope)
                    mejoraPorMes = tope;

                reporte.MejoraPorMes = Math.Round(mejoraPorMes, 2);
            }

            // 3) Proyeccion: el RP menos lo que mejoraria hasta la fecha
            //    objetivo si sigue a este ritmo (si no viene mejorando, se
            //    proyecta su RP tal cual)
            decimal meses = Math.Max(0m, (decimal)(fechaObjetivo.Date - DateTime.Today).TotalDays / DiasPorMes);
            decimal mejoraEsperada = Math.Max(0m, mejoraPorMes) * meses;
            decimal proyeccion = Math.Round(rp - mejoraEsperada, 2);
            reporte.Proyeccion = FormatearSegundos(proyeccion);

            // 4) Veredicto: comparamos el objetivo contra la proyeccion
            //    brecha > 0 = el objetivo es mas rapido que lo proyectado
            decimal brecha = (proyeccion - objetivo) / proyeccion;
            if (brecha <= 0)
                reporte.Veredicto = "Alcanzable";
            else if (brecha <= UmbralPocoRealista)
                reporte.Veredicto = "Ambiciosa";
            else
                reporte.Veredicto = "Poco realista";

            reporte.Conclusion = ArmarConclusion(reporte, rp, objetivo, mejoraPorMes, brecha);
            return reporte;
        }

        // Regresion lineal (minimos cuadrados) de los tiempos contra los dias.
        // Devuelve la pendiente en segundos por dia, o null si no alcanza
        // con los datos (hacen falta 3 marcas en al menos 2 fechas distintas).
        // "Tendencia actual" = las marcas de los ultimos 12 meses antes de
        // la marca mas reciente; si ahi hay menos de 3, se usan todas.
        private decimal? CalcularPendienteSegundosPorDia(List<Marca> marcas)
        {
            var motor = MotorPasajes.GetInstancia();

            DateTime masReciente = marcas.Max(m => m.Fecha);
            var usadas = marcas.Where(m => m.Fecha >= masReciente.AddMonths(-12)).ToList();
            if (usadas.Count < 3)
                usadas = marcas;
            if (usadas.Count < 3)
                return null;

            DateTime origen = usadas.Min(m => m.Fecha);
            var x = usadas.Select(m => (decimal)(m.Fecha - origen).TotalDays).ToList();
            var y = usadas.Select(m => motor.ConvertirASegundos(m.Tiempo)).ToList();

            decimal promedioX = x.Average();
            decimal promedioY = y.Average();
            decimal numerador = 0;
            decimal denominador = 0;

            for (int i = 0; i < x.Count; i++)
            {
                numerador += (x[i] - promedioX) * (y[i] - promedioY);
                denominador += (x[i] - promedioX) * (x[i] - promedioX);
            }

            // Todas las marcas el mismo dia: no se puede trazar una recta
            if (denominador == 0)
                return null;

            return numerador / denominador;
        }

        // Explicacion en palabras del resultado (sirve para la pantalla y el PDF)
        private string ArmarConclusion(ReporteViabilidad r, decimal rp, decimal objetivo,
                                       decimal mejoraPorMes, decimal brecha)
        {
            var texto = new List<string>();

            decimal diferencia = rp - objetivo;
            if (diferencia > 0)
                texto.Add($"El objetivo es {FormatearDecimal(diferencia)} s más rápido que su RP ({r.RecordPersonal}).");
            else if (diferencia < 0)
                texto.Add($"El objetivo es {FormatearDecimal(-diferencia)} s más lento que su RP ({r.RecordPersonal}): ya lo nadó.");
            else
                texto.Add($"El objetivo es igual a su RP ({r.RecordPersonal}).");

            if (!r.TieneTendencia)
                texto.Add("No hay marcas suficientes para calcular una tendencia (hacen falta al menos 3, en fechas distintas), así que se compara solo contra el RP.");
            else if (mejoraPorMes > 0)
                texto.Add($"Viene mejorando {FormatearDecimal(mejoraPorMes)} s por mes; a ese ritmo llegaría a {r.Proyeccion} para el {r.FechaObjetivo:dd/MM/yyyy}.");
            else
                texto.Add("En el último año sus tiempos no vienen bajando, así que la proyección es su RP actual.");

            if (r.Veredicto == "Alcanzable")
            {
                texto.Add("La meta es ALCANZABLE.");
            }
            else
            {
                string porcentaje = FormatearDecimal(brecha * 100);

                // Contra que se compara: la proyeccion (si viene mejorando) o el RP
                string referencia = mejoraPorMes > 0 ? "la proyección de su tendencia" : "su RP actual";

                if (r.Veredicto == "Ambiciosa")
                    texto.Add($"La meta es AMBICIOSA: está {porcentaje} % por debajo de {referencia}.");
                else
                    texto.Add($"La meta es POCO REALISTA: está {porcentaje} % por debajo de {referencia} (el umbral es 3 %).");

                if (mejoraPorMes > 0 && diferencia > 0)
                {
                    decimal mesesNecesarios = Math.Ceiling(diferencia / mejoraPorMes);
                    texto.Add($"Al ritmo actual, alcanzaría el objetivo en unos {mesesNecesarios:0} meses.");
                }
            }

            return string.Join(" ", texto);
        }

        // ================== RF-06.2 Evolucion por estilo ==================

        // estilo = null o "" para incluir todos los estilos
        public ReporteEvolucion GenerarReporteEvolucion(
            int idAtleta, int largoPileta, string? estilo, DateTime desde, DateTime hasta)
        {
            if (desde.Date > hasta.Date)
                throw new ArgumentException("La fecha \"desde\" no puede ser posterior a la fecha \"hasta\".");

            var motor = MotorPasajes.GetInstancia();

            var marcas = _marcaDAL.ObtenerMarcasDeAtleta(idAtleta, largoPileta, desde.Date, hasta.Date);

            if (!string.IsNullOrEmpty(estilo))
                marcas = marcas.Where(m => m.Prueba?.Estilo?.Nombre == estilo).ToList();

            // Pre-condicion de RF-06: tiene que haber marcas en el periodo
            if (marcas.Count == 0)
                throw new InvalidOperationException("El atleta no tiene marcas en ese período con la pileta y el estilo elegidos.");

            var reporte = new ReporteEvolucion
            {
                LargoPileta = largoPileta,
                Estilo = string.IsNullOrEmpty(estilo) ? "Todos" : estilo,
                Desde = desde.Date,
                Hasta = hasta.Date
            };

            // Una fila (y una linea del grafico) por cada prueba,
            // ordenadas por estilo y distancia
            var porPrueba = marcas
                .GroupBy(m => m.IdPrueba)
                .OrderBy(g => g.First().Prueba?.IdEstilo)
                .ThenBy(g => g.First().Prueba?.Distancia);

            foreach (var grupo in porPrueba)
            {
                var lista = grupo.OrderBy(m => m.Fecha).ToList();
                string nombre = lista[0].Prueba?.Nombre ?? $"Prueba {grupo.Key}";
                var segundos = lista.Select(m => motor.ConvertirASegundos(m.Tiempo)).ToList();

                decimal primera = segundos[0];
                decimal mejor = segundos.Min();
                decimal ultima = segundos[segundos.Count - 1];

                // % de mejora respecto de la primera marca del periodo
                decimal mejoraMejor = (primera - mejor) / primera * 100;
                decimal mejoraUltima = (primera - ultima) / primera * 100;

                reporte.Resumen.Add(new ResumenEvolucionPrueba
                {
                    Prueba = nombre,
                    Marcas = lista.Count,
                    Primera = FormatearSegundos(primera),
                    Mejor = FormatearSegundos(mejor),
                    Ultima = FormatearSegundos(ultima),
                    Mejora = lista.Count < 2 ? "-" : FormatearDecimal(mejoraMejor) + " %",
                    Estado = ClasificarEvolucion(lista.Count, mejoraMejor, mejoraUltima)
                });

                // Con una sola marca no hay linea que dibujar
                if (lista.Count >= 2)
                {
                    reporte.Series.Add(new SerieEvolucion
                    {
                        Prueba = nombre,
                        Fechas = lista.Select(m => m.Fecha).ToArray(),
                        PorcentajesMejora = segundos
                            .Select(s => (double)Math.Round((primera - s) / primera * 100, 2))
                            .ToArray()
                    });
                }
            }

            return reporte;
        }

        // Criterio de clasificacion (documentarlo en RF-06.2):
        // - En retroceso: la ultima marca es mas de 0.5 % mas lenta que la primera
        // - Mejora notable: la mejor marca bajo 2 % o mas
        // - Mejora: la mejor marca bajo entre 0.5 % y 2 %
        // - Estancado: bajo menos de 0.5 %
        private string ClasificarEvolucion(int cantidadMarcas, decimal mejoraMejor, decimal mejoraUltima)
        {
            if (cantidadMarcas < 2) return "Sin datos suficientes";
            if (mejoraUltima < -0.5m) return "En retroceso";
            if (mejoraMejor >= 2m) return "Mejora notable";
            if (mejoraMejor >= 0.5m) return "Mejora";
            return "Estancado";
        }

        // ---------- Formatos ----------

        // 62.30 -> "01:02.30"   28.5 -> "28.50"
        private string FormatearSegundos(decimal segundos)
        {
            var motor = MotorPasajes.GetInstancia();
            return motor.FormatearParaMostrar(motor.ConvertirDeSegundos(segundos));
        }

        // Siempre con punto decimal, aunque Windows este en español
        private string FormatearDecimal(decimal valor)
        {
            return valor.ToString("0.00", CultureInfo.InvariantCulture);
        }
    }
}
