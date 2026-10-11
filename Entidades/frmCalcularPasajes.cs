// Proyecto: NatSys.UI
//
// Pantalla de CU-01 (Calcular plan de pasajes). Sigue el diagrama de
// secuencias: atletas activos -> pruebas del atleta -> validar formato ->
// record personal -> calculo del motor -> mostrar -> guardar.
//
// Toda la matematica la hace MotorPasajes (patron Strategy). Esta
// pantalla solo junta los datos, muestra el resultado y pide guardar.

using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmCalcularPasajes : Form
    {
        // El plan se guarda a nombre del entrenador que inicio sesion
        private readonly Entrenador _entrenador;

        private List<Atleta> _atletas = new List<Atleta>();
        private List<Prueba> _pruebasDelAtleta = new List<Prueba>();

        // El ultimo plan calculado, todavia SIN guardar. Es null si no se
        // calculo nada o si se cambio algun dato despues de calcular.
        private PlanPasaje? _planCalculado;

        public frmCalcularPasajes(Entrenador entrenador)
        {
            InitializeComponent();
            _entrenador = entrenador;

            CargarAtletas();
            cmbLargoPileta.SelectedIndex = 0; // "25"
            LimpiarResultado();
        }

        // CU-01 paso 2: solo atletas activos
        private void CargarAtletas()
        {
            _atletas = GestorAtleta.GetInstancia().GetAtletasActivos();
            foreach (var atleta in _atletas)
                cmbAtleta.Items.Add(new OpcionCombo(atleta.IdPersona, atleta.GetNombreCompleto()));
        }

        // CU-01 pasos 3 y 4: al elegir un atleta se cargan SOLO sus pruebas
        private void cmbAtleta_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPrueba.Items.Clear();
            _pruebasDelAtleta = new List<Prueba>();

            var atleta = AtletaSeleccionado();
            if (atleta != null)
            {
                _pruebasDelAtleta = GestorAtleta.GetInstancia().ObtenerPruebasDeAtleta(atleta.IdPersona);
                foreach (var prueba in _pruebasDelAtleta)
                    cmbPrueba.Items.Add(new OpcionCombo(prueba.IdPrueba, prueba.Nombre));

                // Pre-condicion del CU-01: el atleta tiene que tener pruebas
                if (_pruebasDelAtleta.Count == 0)
                    MessageBox.Show("Este atleta no tiene pruebas asignadas. Asignáselas desde Gestión de atletas.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            MostrarPlanVigente();
            LimpiarResultado();
        }

        private void cmbPrueba_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarPlanVigente();
            LimpiarResultado();
        }

        // Si cambia la pileta o la marca objetivo, el plan calculado ya no
        // corresponde a lo que esta en pantalla: se borra para que no se
        // pueda guardar un plan que no coincide con los datos.
        private void cmbLargoPileta_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarResultado();
        }

        private void txtMarcaObjetivo_TextChanged(object sender, EventArgs e)
        {
            LimpiarResultado();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            LimpiarResultado();

            var atleta = AtletaSeleccionado();
            var prueba = PruebaSeleccionada();
            if (atleta == null || prueba == null)
            {
                MessageBox.Show("Elegí el atleta y la prueba.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var motor = MotorPasajes.GetInstancia();
            string marcaObjetivo = txtMarcaObjetivo.Text.Trim();
            int largoPileta = int.Parse((string)cmbLargoPileta.SelectedItem!);

            // CU-01 paso 8 / CA-01: si el formato esta mal, NO se calcula nada
            if (!motor.ValidarFormatoTiempo(marcaObjetivo))
            {
                MessageBox.Show(MotorPasajes.MensajeFormatoTiempo, "Formato inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMarcaObjetivo.Focus();
                return;
            }

            try
            {
                // Pasos 9 y 10: el motor verifica que el atleta este activo
                // y tenga la prueba, elige la estrategia segun el estilo y
                // la distancia, y reparte el tiempo en tramos.
                _planCalculado = motor.CrearPlanPasajes(atleta, prueba, _entrenador, largoPileta, marcaObjetivo);
            }
            catch (ArgumentException ex) // ej: 100 Combinado en pileta de 50
            {
                MessageBox.Show(ex.Message, "No se puede calcular", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (InvalidOperationException ex) // CA-03: atleta inactivo / prueba no asignada
            {
                MessageBox.Show(ex.Message, "No se puede calcular", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Paso 11: tabla de pasajes + comparativa con el RP
            MostrarPasajes(_planCalculado);
            MostrarComparativaRP(atleta, prueba, largoPileta, marcaObjetivo);
            btnGuardar.Enabled = true;
        }

        // Arma la tabla con el parcial de cada tramo y el tiempo acumulado.
        // Usamos un DataTable: es una tabla "en memoria" con columnas y
        // filas, que el DataGridView sabe mostrar directamente.
        private void MostrarPasajes(PlanPasaje plan)
        {
            var motor = MotorPasajes.GetInstancia();

            var tabla = new DataTable();
            tabla.Columns.Add("Tramo", typeof(int));
            tabla.Columns.Add("Metros", typeof(string));
            tabla.Columns.Add("Parcial", typeof(string));
            tabla.Columns.Add("Acumulado", typeof(string));

            decimal segundosAcumulados = 0;
            int metrosAcumulados = 0;

            foreach (var pasaje in plan.Pasajes.OrderBy(p => p.NroTramo))
            {
                segundosAcumulados += motor.ConvertirASegundos(pasaje.Tiempo);
                metrosAcumulados += pasaje.Distancia; // cada tramo mide un largo de pileta

                tabla.Rows.Add(
                    pasaje.NroTramo,
                    metrosAcumulados + " m",
                    motor.FormatearParaMostrar(pasaje.Tiempo),
                    motor.FormatearParaMostrar(motor.ConvertirDeSegundos(segundosAcumulados)));
            }

            dgvPasajes.DataSource = tabla;
        }

        // CU-01 paso 11 + EXT-01 (advertir marca poco realista).
        private void MostrarComparativaRP(Atleta atleta, Prueba prueba, int largoPileta, string marcaObjetivo)
        {
            var motor = MotorPasajes.GetInstancia();
            var rp = GestorMarcas.GetInstancia().ObtenerRecordPersonal(atleta.IdPersona, prueba.IdPrueba, largoPileta);

            // CA-02: sin marcas previas (SV) el plan se muestra igual, pero
            // sin comparativa
            if (rp.EsSV())
            {
                lblRecordPersonal.Text = $"Récord personal en pileta de {largoPileta} m: SV (sin marcas previas)";
                lblViabilidad.Text = "";
                return;
            }

            lblRecordPersonal.Text =
                $"Récord personal en pileta de {largoPileta} m: {motor.FormatearParaMostrar(rp.Tiempo)} ({rp.Fecha:dd/MM/yyyy})";

            string viabilidad = motor.EvaluarViabilidad(marcaObjetivo, rp);

            // Diferencia en segundos: positiva = el objetivo es mas rapido que el RP
            decimal diferencia = motor.ConvertirASegundos(rp.Tiempo) - motor.ConvertirASegundos(marcaObjetivo);
            string segundos = Math.Abs(diferencia).ToString("0.00", CultureInfo.InvariantCulture);
            string comparacion;
            if (diferencia > 0)
                comparacion = $"{segundos} s más rápido que su RP";
            else if (diferencia < 0)
                comparacion = $"{segundos} s más lento que su RP";
            else
                comparacion = "igual a su RP";

            lblViabilidad.Text = $"Viabilidad: {viabilidad} (objetivo {comparacion})";

            // Color segun el resultado: verde, naranja o rojo
            if (viabilidad == "Poco realista")
                lblViabilidad.ForeColor = Color.Firebrick;
            else if (viabilidad == "Ambiciosa")
                lblViabilidad.ForeColor = Color.DarkOrange;
            else
                lblViabilidad.ForeColor = Color.ForestGreen;

            // EXT-01 (FA-01 de RF-01): advertencia. El plan igual se calculo.
            if (viabilidad == "Poco realista")
            {
                MessageBox.Show(
                    "La marca objetivo mejora el récord personal del atleta por encima del umbral definido. " +
                    "El plan se calculó igual, pero la meta puede ser poco realista.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Muestra si ya hay un plan guardado ("Disponible") para ese atleta
        // y esa prueba. Es el que se va a reemplazar si se guarda uno nuevo.
        private void MostrarPlanVigente()
        {
            var atleta = AtletaSeleccionado();
            var prueba = PruebaSeleccionada();
            if (atleta == null || prueba == null)
            {
                lblPlanVigente.Text = "";
                return;
            }

            var motor = MotorPasajes.GetInstancia();
            var plan = motor.ObtenerPlanVigente(atleta.IdPersona, prueba.IdPrueba);

            if (plan == null)
                lblPlanVigente.Text = "Plan vigente: no tiene ninguno guardado para esta prueba.";
            else
                lblPlanVigente.Text =
                    $"Plan vigente: objetivo {motor.FormatearParaMostrar(plan.MarcaObjetivo)} " +
                    $"en pileta de {plan.LargoPileta} m ({plan.Fecha:dd/MM/yyyy}).";
        }

        // CU-01 paso 12 + EXT-02 (pasos 28 a 38 del diagrama de secuencias)
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (_planCalculado == null) return;

            var motor = MotorPasajes.GetInstancia();

            // Si ya hay un plan vigente, avisamos que va a quedar reemplazado
            var planVigente = motor.ObtenerPlanVigente(_planCalculado.IdAtleta, _planCalculado.IdPrueba);
            if (planVigente != null)
            {
                var respuesta = MessageBox.Show(
                    $"Este atleta ya tiene un plan vigente para esta prueba (objetivo {motor.FormatearParaMostrar(planVigente.MarcaObjetivo)}).\n" +
                    "Si guardás el nuevo, el anterior queda como \"Reemplazado\" en el historial.\n\n¿Guardar igual?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes) return;
            }

            motor.GuardarPlan(_planCalculado);
            MessageBox.Show("Plan guardado.", "Listo");

            btnGuardar.Enabled = false; // evita guardar dos veces el mismo plan
            MostrarPlanVigente();
        }

        // "Descartar": no se guarda nada y se vuelve a empezar (post-condicion
        // del CU-01). Se mantienen el atleta y la prueba elegidos.
        private void btnDescartar_Click(object sender, EventArgs e)
        {
            txtMarcaObjetivo.Clear();
            LimpiarResultado();
            txtMarcaObjetivo.Focus();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Borra el plan calculado y todo lo que se mostraba de el
        private void LimpiarResultado()
        {
            _planCalculado = null;
            dgvPasajes.DataSource = null;
            lblRecordPersonal.Text = "";
            lblViabilidad.Text = "";
            btnGuardar.Enabled = false;
        }

        private Atleta? AtletaSeleccionado()
        {
            var opcion = cmbAtleta.SelectedItem as OpcionCombo;
            if (opcion == null) return null;
            return _atletas.FirstOrDefault(a => a.IdPersona == opcion.Id);
        }

        private Prueba? PruebaSeleccionada()
        {
            var opcion = cmbPrueba.SelectedItem as OpcionCombo;
            if (opcion == null) return null;
            return _pruebasDelAtleta.FirstOrDefault(p => p.IdPrueba == opcion.Id);
        }
    }
}