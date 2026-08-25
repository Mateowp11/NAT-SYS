// Proyecto: NatSys.BLL
//
// Implementa CU-01 (Calcular plan de pasajes) completo, incluyendo la
// logica de tu diagrama de secuencias: valida formato, calcula pasajes
// usando la estrategia correspondiente, y guarda reemplazando el plan
// vigente si existia uno.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using NatSys.DAL;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class MotorPasajes
    {
        private static MotorPasajes _instancia;
        private readonly DALPlanesPasaje _planDAL;

        // Estrategias disponibles (patron Strategy). La eleccion depende de
        // si hay datos y de si la prueba es de velocidad pura o no.
        private readonly IEstrategiaDistribucion _estrategiaParabolica = new EstrategiaDistribucionEscalonada();
        private readonly IEstrategiaDistribucion _estrategiaSprint = new EstrategiaDistribucionSprint();
        private readonly IEstrategiaDistribucion _estrategiaResguardo = new EstrategiaDistribucionUniforme();

        private MotorPasajes(string connectionString)
        {
            _planDAL = new DALPlanesPasaje(connectionString);
        }

        public static void Inicializar(string connectionString)
        {
            if (_instancia == null)
                _instancia = new MotorPasajes(connectionString);
        }

        public static MotorPasajes GetInstancia()
        {
            if (_instancia == null)
                throw new InvalidOperationException("MotorPasajes no fue inicializado.");
            return _instancia;
        }

        // ---------- Formato y conversion de tiempos ----------

        // Formato esperado: mm:ss.cc (CU-01, regla de negocio)
        public bool ValidarFormatoTiempo(string tiempo)
        {
            if (string.IsNullOrWhiteSpace(tiempo)) return false;
            return Regex.IsMatch(tiempo, @"^\d{1,2}:\d{2}\.\d{2}$");
        }

        public decimal ConvertirASegundos(string tiempo)
        {
            var partes = tiempo.Split(':', '.');
            decimal minutos = decimal.Parse(partes[0], CultureInfo.InvariantCulture);
            decimal segundos = decimal.Parse(partes[1], CultureInfo.InvariantCulture);
            decimal centesimas = decimal.Parse(partes[2], CultureInfo.InvariantCulture);
            return minutos * 60 + segundos + centesimas / 100;
        }

        public string ConvertirDeSegundos(decimal segundosTotales)
        {
            int minutos = (int)(segundosTotales / 60);
            decimal resto = segundosTotales - (minutos * 60);
            int segundos = (int)resto;
            int centesimas = (int)Math.Round((resto - segundos) * 100);

            if (centesimas >= 100)
            {
                centesimas = 0;
                segundos++;
            }
            if (segundos >= 60)
            {
                segundos = 0;
                minutos++;
            }

            return $"{minutos:00}:{segundos:00}.{centesimas:00}";
        }

        // ---------- El calculo principal ----------

        public List<Pasaje> CalcularPasajes(Prueba prueba, int largoPileta, string marcaObjetivo)
        {
            if (!ValidarFormatoTiempo(marcaObjetivo))
                throw new ArgumentException("La marca objetivo debe tener el formato mm:ss.cc");

            int nroTramos = prueba.GetNroTramos(largoPileta);
            if (nroTramos <= 0)
                throw new ArgumentException("El largo de pileta no es compatible con la distancia de la prueba.");

            decimal totalSegundos = ConvertirASegundos(marcaObjetivo);

            // Primero busca en la tabla de perfiles elite (diferenciada por
            // estilo Y largo de pileta). Si no hay dato para esa
            // combinacion, cae a los coeficientes propios del Estilo en la
            // base. Si tampoco hay, cae a reparto uniforme.
            decimal[] coeficientes = PerfilesRitmoElite.ObtenerCoeficientes(prueba.Estilo.Nombre, largoPileta)
                ?? prueba.GetCoeficientesEstilo();

            IEstrategiaDistribucion estrategia;

            if (coeficientes == null || coeficientes.Length != 3)
            {
                // Sin datos: reparto uniforme como ultimo resguardo
                estrategia = _estrategiaResguardo;
            }
            else if (prueba.Distancia <= 100)
            {
                // Velocidad pura (50m/100m): ritmo positivo, sin
                // recuperacion al cierre. OJO: la clasificacion es por
                // DISTANCIA de la prueba, no por nroTramos - un 100m en
                // pileta corta tiene los mismos 4 tramos que un 200m en
                // pileta larga, pero son carreras completamente distintas.
                estrategia = _estrategiaSprint;
            }
            else
            {
                // 200m en adelante: patron parabolico clasico
                estrategia = _estrategiaParabolica;
            }

            List<decimal> pesos = estrategia.CalcularPesos(nroTramos, coeficientes);

            return DistribuirTiempoPorTramos(totalSegundos, pesos, largoPileta, nroTramos);
        }

        // Reparte el tiempo proporcional a los pesos, y ajusta el ULTIMO
        // tramo para que la suma de todo de EXACTO la marca objetivo (los
        // redondeos de centesimas podrian dejar una diferencia minima si
        // no se corrige asi).
        private List<Pasaje> DistribuirTiempoPorTramos(
            decimal totalSegundos, List<decimal> pesos, int largoPileta, int nroTramos)
        {
            decimal sumaPesos = pesos.Sum();
            var pasajes = new List<Pasaje>();
            decimal acumulado = 0;

            for (int i = 0; i < nroTramos; i++)
            {
                decimal segundosTramo;

                if (i < nroTramos - 1)
                {
                    decimal proporcion = pesos[i] / sumaPesos;
                    segundosTramo = Math.Round(totalSegundos * proporcion, 2);
                    acumulado += segundosTramo;
                }
                else
                {
                    segundosTramo = totalSegundos - acumulado;
                }

                pasajes.Add(new Pasaje
                {
                    NroTramo = i + 1,
                    Distancia = largoPileta,
                    Tiempo = ConvertirDeSegundos(segundosTramo)
                });
            }

            return pasajes;
        }

        // Paso "validarSumaMarcaObj" del diagrama de secuencias: confirma
        // que el calculo dio exacto (deberia ser siempre true si
        // DistribuirTiempoPorTramos funciono bien - sirve como control).
        public bool ValidarSumaMarcaObjetivo(List<Pasaje> pasajes, string marcaObjetivo)
        {
            decimal sumaCalculada = pasajes.Sum(p => p.ConvertirASegundos());
            decimal objetivo = ConvertirASegundos(marcaObjetivo);
            return Math.Abs(sumaCalculada - objetivo) < 0.01m;
        }

        // ---------- Viabilidad (FA-01 de tu CU-01) ----------

        public string EvaluarViabilidad(string marcaObjetivo, Marca rp)
        {
            if (rp == null || rp.EsSV())
                return "Sin historial previo para comparar";

            decimal objetivoSeg = ConvertirASegundos(marcaObjetivo);
            decimal rpSeg = ConvertirASegundos(rp.Tiempo);

            decimal mejoraRelativa = (rpSeg - objetivoSeg) / rpSeg;

            if (mejoraRelativa <= 0) return "Alcanzable";
            if (mejoraRelativa <= 0.03m) return "Ambiciosa";
            return "Poco realista";
        }

        // ---------- Armado y guardado del plan completo ----------

        public PlanPasaje CrearPlanPasajes(
            Atleta atleta, Prueba prueba, Entrenador entrenador, int largoPileta, string marcaObjetivo)
        {
            // Precondiciones de CU-01
            if (!atleta.EstaActivo())
                throw new InvalidOperationException("No se puede calcular un plan para un atleta inactivo.");

            if (!atleta.Pruebas.Any(p => p.IdPrueba == prueba.IdPrueba))
                throw new InvalidOperationException("El atleta no tiene esta prueba asignada en su perfil.");

            var pasajes = CalcularPasajes(prueba, largoPileta, marcaObjetivo);

            return new PlanPasaje
            {
                IdAtleta = atleta.IdPersona,
                IdPrueba = prueba.IdPrueba,
                IdEntrenador = entrenador.IdPersona,
                MarcaObjetivo = marcaObjetivo,
                LargoPileta = largoPileta,
                Fecha = DateTime.Now,
                Estado = "Disponible",
                Pasajes = pasajes
            };
        }

        public void GuardarPlan(PlanPasaje plan) => _planDAL.GuardarPlan(plan);

        public PlanPasaje ObtenerPlanVigente(int idAtleta, int idPrueba) =>
            _planDAL.ObtenerPlanVigente(idAtleta, idPrueba);

        public List<PlanPasaje> ObtenerHistorial(int idAtleta, int idPrueba) =>
            _planDAL.ObtenerHistorial(idAtleta, idPrueba);

        // ---------- Pruebas Combinadas (200/400) ----------
        //
        // Un Combinado no es "un estilo": son 4 piernas de estilos
        // distintos en orden fijo. Reutilizamos CalcularPasajes una vez
        // por pierna, con una Prueba "temporal" en memoria (nunca se
        // persiste) que representa solo esa pierna - asi toda la
        // diferenciacion que ya armamos (Strategy, PerfilesRitmoElite,
        // pileta larga/corta) se aplica igual dentro de cada pierna.

        public PlanPasaje CrearPlanPasajesCombinado(
            Atleta atleta, Prueba pruebaCombinado, Entrenador entrenador, int largoPileta, string marcaObjetivo)
        {
            if (!atleta.EstaActivo())
                throw new InvalidOperationException("No se puede calcular un plan para un atleta inactivo.");

            if (!atleta.Pruebas.Any(p => p.IdPrueba == pruebaCombinado.IdPrueba))
                throw new InvalidOperationException("El atleta no tiene esta prueba asignada en su perfil.");

            if (pruebaCombinado.Distancia != 200 && pruebaCombinado.Distancia != 400)
                throw new ArgumentException("El Combinado solo esta definido para 200 y 400 metros.");

            if (!ValidarFormatoTiempo(marcaObjetivo))
                throw new ArgumentException("La marca objetivo debe tener el formato mm:ss.cc");

            var pasajes = CalcularPasajesCombinado(pruebaCombinado.Distancia, largoPileta, marcaObjetivo);

            return new PlanPasaje
            {
                IdAtleta = atleta.IdPersona,
                IdPrueba = pruebaCombinado.IdPrueba,
                IdEntrenador = entrenador.IdPersona,
                MarcaObjetivo = marcaObjetivo,
                LargoPileta = largoPileta,
                Fecha = DateTime.Now,
                Estado = "Disponible",
                Pasajes = pasajes
            };
        }

        public List<Pasaje> CalcularPasajesCombinado(int distanciaTotal, int largoPileta, string marcaObjetivo)
        {
            decimal totalSegundos = ConvertirASegundos(marcaObjetivo);
            decimal distanciaPorPierna = distanciaTotal / 4m;

            var pasajes = new List<Pasaje>();
            decimal acumulado = 0;
            int nroTramoGlobal = 1;

            for (int i = 0; i < PerfilesCombinado.OrdenEstilos.Length; i++)
            {
                string nombreEstiloPierna = PerfilesCombinado.OrdenEstilos[i];
                bool esUltimaPierna = i == PerfilesCombinado.OrdenEstilos.Length - 1;

                // La ultima pierna (Crol) absorbe el redondeo, igual que
                // hacemos con el ultimo tramo dentro de una sola prueba.
                decimal segundosPierna = esUltimaPierna
                    ? totalSegundos - acumulado
                    : Math.Round(totalSegundos * PerfilesCombinado.PorcentajePorPierna[nombreEstiloPierna], 2);

                acumulado += segundosPierna;

                var pruebaPierna = new Prueba
                {
                    Distancia = (int)distanciaPorPierna,
                    Estilo = new Estilo { Nombre = nombreEstiloPierna }
                };

                var pasajesPierna = CalcularPasajes(pruebaPierna, largoPileta, ConvertirDeSegundos(segundosPierna));

                foreach (var pasaje in pasajesPierna)
                {
                    pasaje.NroTramo = nroTramoGlobal++;
                    pasajes.Add(pasaje);
                }
            }

            return pasajes;
        }
    }
}