// Proyecto: NatSys.BLL
//
// PATRON FACADE (estructural).
//
// Problema: para calcular un plan de pasajes (CU-01), la pantalla tenia
// que conocer y coordinar TRES clases de la BLL:
//   - GestorAtleta  -> atletas activos y sus pruebas
//   - GestorMarcas  -> record personal
//   - MotorPasajes  -> validar, calcular, evaluar viabilidad, guardar
// y ademas saber en que orden llamarlas.
//
// Solucion: esta fachada ofrece UN solo punto de entrada con pocos
// metodos simples. Por dentro llama a esas tres clases en el orden de tu
// diagrama de secuencias. No agrega reglas de negocio nuevas: solo
// coordina (las reglas siguen en los gestores y en el motor).
//
// Roles del patron:
//   Fachada     -> FachadaPasajes
//   Subsistema  -> GestorAtleta, GestorMarcas, MotorPasajes
//   Cliente     -> frmCalcularPasajes (calcular y guardar) y
//                  frmPlanesAtleta (ver y borrar planes guardados)

using System;
using System.Collections.Generic;
using System.Linq;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class FachadaPasajes
    {
        // Las clases del subsistema. Son Singleton, asi que la fachada
        // solo pide la instancia de cada una.
        private readonly GestorAtleta _gestorAtleta;
        private readonly GestorMarcas _gestorMarcas;
        private readonly MotorPasajes _motor;

        public FachadaPasajes()
        {
            _gestorAtleta = GestorAtleta.GetInstancia();
            _gestorMarcas = GestorMarcas.GetInstancia();
            _motor = MotorPasajes.GetInstancia();
        }

        // ---------- Datos para llenar la pantalla ----------

        // CU-01 paso 2
        public List<Atleta> ObtenerAtletasActivos() => _gestorAtleta.GetAtletasActivos();

        // CU-01 paso 4: solo las pruebas asignadas a ese atleta
        public List<Prueba> ObtenerPruebasDeAtleta(int idAtleta) => _gestorAtleta.ObtenerPruebasDeAtleta(idAtleta);

        // El plan guardado ("Disponible") para ese atleta y prueba, o null
        public PlanPasaje? ObtenerPlanVigente(int idAtleta, int idPrueba) => _motor.ObtenerPlanVigente(idAtleta, idPrueba);

        // ---------- Tiempos ----------

        public bool ValidarFormatoTiempo(string tiempo) => _motor.ValidarFormatoTiempo(tiempo);

        public string MensajeFormatoTiempo => MotorPasajes.MensajeFormatoTiempo;

        // "00:28.50" -> "28.50"
        public string FormatearTiempo(string tiempo) => _motor.FormatearParaMostrar(tiempo);

        // ---------- El calculo completo (CU-01 pasos 8 a 11) ----------

        public ResultadoCalculoPasajes CalcularPlan(
            Atleta atleta, Prueba prueba, Entrenador entrenador, int largoPileta, string marcaObjetivo)
        {
            // Paso 8 (CA-01): si el formato esta mal, no se calcula nada
            if (!_motor.ValidarFormatoTiempo(marcaObjetivo))
                throw new ArgumentException(MotorPasajes.MensajeFormatoTiempo);

            // Pasos 9 y 10: el motor verifica atleta y prueba, elige la
            // estrategia (Strategy) y reparte el tiempo en tramos
            PlanPasaje plan = _motor.CrearPlanPasajes(atleta, prueba, entrenador, largoPileta, marcaObjetivo);

            var resultado = new ResultadoCalculoPasajes
            {
                Plan = plan,
                Filas = ArmarFilas(plan),
                LargoPileta = largoPileta
            };

            // Paso 16 del diagrama: record personal en esa prueba y pileta
            Marca rp = _gestorMarcas.ObtenerRecordPersonal(atleta.IdPersona, prueba.IdPrueba, largoPileta);

            // CA-02: si no tiene marcas previas (SV), no hay comparacion
            if (!rp.EsSV())
            {
                resultado.TieneRecordPersonal = true;
                resultado.TiempoRecordPersonal = _motor.FormatearParaMostrar(rp.Tiempo);
                resultado.FechaRecordPersonal = rp.Fecha;
                resultado.Viabilidad = _motor.EvaluarViabilidad(marcaObjetivo, rp);
                resultado.DiferenciaConRP =
                    _motor.ConvertirASegundos(rp.Tiempo) - _motor.ConvertirASegundos(marcaObjetivo);
            }

            return resultado;
        }

        // CU-01 paso 12 + EXT-02: guarda y deja el anterior como "Reemplazado"
        public void GuardarPlan(PlanPasaje plan) => _motor.GuardarPlan(plan);

        // ---------- Planes guardados (frmPlanesAtleta) ----------

        public List<PlanPasaje> ObtenerPlanesDeAtleta(int idAtleta) => _motor.ObtenerPlanesDeAtleta(idAtleta);

        // Las mismas filas (tramo, metros, parcial, acumulado) que muestra la
        // pantalla de calculo, pero de un plan que ya estaba guardado
        public List<FilaPasaje> ObtenerFilasDePlan(PlanPasaje plan) => ArmarFilas(plan);

        public void EliminarPlan(int idPlan) => _motor.EliminarPlan(idPlan);

        // Arma las filas de la grilla: parcial de cada tramo y tiempo acumulado
        private List<FilaPasaje> ArmarFilas(PlanPasaje plan)
        {
            var filas = new List<FilaPasaje>();
            decimal segundosAcumulados = 0;
            int metrosAcumulados = 0;

            foreach (var pasaje in plan.Pasajes.OrderBy(p => p.NroTramo))
            {
                segundosAcumulados += _motor.ConvertirASegundos(pasaje.Tiempo);
                metrosAcumulados += pasaje.Distancia; // cada tramo mide un largo de pileta

                filas.Add(new FilaPasaje
                {
                    Tramo = pasaje.NroTramo,
                    Metros = metrosAcumulados + " m",
                    Parcial = _motor.FormatearParaMostrar(pasaje.Tiempo),
                    Acumulado = _motor.FormatearParaMostrar(_motor.ConvertirDeSegundos(segundosAcumulados))
                });
            }

            return filas;
        }
    }
}