// Proyecto: NatSys.BLL
//
// Implementa CU-01 (Calcular plan de pasajes) completo, incluyendo la
// logica de tu diagrama de secuencias: valida formato, calcula pasajes
// usando la estrategia correspondiente (primero el perfil real de los
// finalistas de Mundiales / JJOO) y guarda reemplazando el plan vigente si
// existia uno.

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
        private static MotorPasajes? _instancia;

        // Un solo texto para todos los errores de formato de tiempo
        public const string MensajeFormatoTiempo =
            "El tiempo debe tener el formato ss.cc (por ejemplo 28.50) o mm:ss.cc (por ejemplo 01:05.32).";
        private readonly DALPlanesPasaje _planDAL;

        // Estrategias disponibles (patron Strategy). La eleccion depende de
        // si hay datos reales para la prueba y, si no los hay, de si es de
        // velocidad pura o no.
        private readonly IEstrategiaDistribucion _estrategiaPerfilReal = new EstrategiaDistribucionPerfilReal();
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

        // Se aceptan dos formatos:
        //   mm:ss.cc  ->  01:05.32  (los segundos van de 00 a 59)
        //   ss.cc     ->  28.50     (para tiempos de menos de un minuto)
        // El "|" del patron significa "o": o hay minutos y segundos, o
        // hay solo segundos. En los dos casos terminan en .cc
        public bool ValidarFormatoTiempo(string tiempo)
        {
            if (string.IsNullOrWhiteSpace(tiempo)) return false;
            return Regex.IsMatch(tiempo, @"^(\d{1,2}:[0-5]\d|[0-5]?\d)\.\d{2}$");
        }

        public decimal ConvertirASegundos(string tiempo)
        {
            // "28.50" no tiene minutos: le agregamos "0:" adelante y queda
            // "0:28.50", asi se procesa igual que el formato largo
            if (!tiempo.Contains(':'))
                tiempo = "0:" + tiempo;

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

        // En la base los tiempos se guardan SIEMPRE como mm:ss.cc (asi
        // ordenarlos o compararlos como texto no falla). Este metodo es
        // solo para mostrarlos en pantalla como los escribe un nadador:
        //   "00:28.50" -> "28.50"      "01:05.32" -> "01:05.32"
        public string FormatearParaMostrar(string tiempo)
        {
            if (!ValidarFormatoTiempo(tiempo)) return tiempo; // si viene algo raro, se muestra tal cual

            decimal segundosTotales = ConvertirASegundos(tiempo);
            if (segundosTotales >= 60)
                return ConvertirDeSegundos(segundosTotales);

            // InvariantCulture: asegura el punto decimal ("28.50") aunque
            // Windows este configurado en español (que usaria "28,50")
            return segundosTotales.ToString("0.00", CultureInfo.InvariantCulture);
        }

        // ---------- El calculo principal ----------

        public List<Pasaje> CalcularPasajes(Prueba prueba, int largoPileta, string marcaObjetivo)
        {
            if (!ValidarFormatoTiempo(marcaObjetivo))
                throw new ArgumentException(MensajeFormatoTiempo);

            int nroTramos = prueba.GetNroTramos(largoPileta);
            if (nroTramos <= 0)
                throw new ArgumentException("El largo de pileta no es compatible con la distancia de la prueba.");

            decimal totalSegundos = ConvertirASegundos(marcaObjetivo);

            // Defecto D-02 (prueba CN-14): un tiempo 00.00 no es una marca valida
            if (totalSegundos <= 0)
                throw new ArgumentException("La marca objetivo tiene que ser mayor a cero.");

            // Eleccion de la estrategia (patron Strategy), en este orden:
            // 1. Si hay un perfil REAL para esta prueba (parciales de los
            //    finalistas de un Mundial / JJOO), se usa ese.
            // 2. Si no, el modelo teorico con los coeficientes del Estilo
            //    (Sprint para 50/100, Escalonada para 200 en adelante).
            // 3. Si tampoco hay coeficientes, reparto uniforme.
            decimal[]? perfilReal = PerfilesRitmoElite.ObtenerPerfil(prueba.Estilo?.Nombre ?? "", prueba.Distancia, largoPileta);
            decimal[]? coeficientesEstilo = prueba.GetCoeficientesEstilo();

            IEstrategiaDistribucion estrategia;
            decimal[] datos; // lo que recibe la estrategia elegida

            if (perfilReal != null && perfilReal.Length == nroTramos)
            {
                // Caso normal: porcentajes reales de los finalistas
                estrategia = _estrategiaPerfilReal;
                datos = perfilReal;
            }
            else if (coeficientesEstilo == null || coeficientesEstilo.Length != 3)
            {
                // Sin datos de ningun tipo: reparto uniforme como ultimo resguardo
                estrategia = _estrategiaResguardo;
                datos = Array.Empty<decimal>(); // la uniforme no necesita datos
            }
            else if (prueba.Distancia <= 100)
            {
                // Modelo teorico de velocidad pura (50m/100m): el ritmo
                // decae de forma continua, sin recuperacion al cierre.
                estrategia = _estrategiaSprint;
                datos = coeficientesEstilo;
            }
            else
            {
                // Modelo teorico para 200 m en adelante
                estrategia = _estrategiaParabolica;
                datos = coeficientesEstilo;
            }

            List<decimal> pesos = estrategia.CalcularPesos(nroTramos, datos);

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

            // El Combinado tiene reglas propias de distancia y pileta.
            // Requiere que la Prueba venga con su Estilo cargado.
            if (EsCombinado(prueba))
                ValidarCombinado(prueba.Distancia, largoPileta);

            var pasajes = CalcularPasajes(prueba, largoPileta, marcaObjetivo);

            return new PlanPasaje
            {
                IdAtleta = atleta.IdPersona,
                IdPrueba = prueba.IdPrueba,
                IdEntrenador = entrenador.IdPersona,
                MarcaObjetivo = ConvertirDeSegundos(ConvertirASegundos(marcaObjetivo)), // se guarda siempre como mm:ss.cc
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

        // Planes guardados de un atleta (pantalla frmPlanesAtleta)
        public List<PlanPasaje> ObtenerPlanesDeAtleta(int idAtleta) =>
            _planDAL.ObtenerPlanesDeAtleta(idAtleta);

        // Borra un plan guardado con sus tramos. Si era el vigente, esa prueba
        // queda sin plan vigente hasta que se guarde uno nuevo.
        public void EliminarPlan(int idPlan) => _planDAL.EliminarPlan(idPlan);

        // ---------- Pruebas Combinadas (100/200/400) ----------
        //
        // El Combinado ya no necesita un calculo aparte: su perfil real
        // (mariposa, espalda, pecho y crol en ese orden) esta en
        // PerfilesRitmoElite como cualquier otra prueba. Solo quedan las
        // reglas del reglamento, que se controlan antes de calcular.

        private bool EsCombinado(Prueba prueba)
        {
            return string.Equals(prueba.Estilo?.Nombre, "Combinado", StringComparison.OrdinalIgnoreCase);
        }

        private void ValidarCombinado(int distancia, int largoPileta)
        {
            if (distancia != 100 && distancia != 200 && distancia != 400)
                throw new ArgumentException("El Combinado solo esta definido para 100, 200 y 400 metros.");

            // Reglamento: el 100 Combinado solo se nada en pileta corta
            // (25 m por estilo = un largo por pierna)
            if (distancia == 100 && largoPileta != 25)
                throw new ArgumentException("El 100 Combinado solo existe en pileta de 25 metros.");
        }
    }
}