// Proyecto: NatSys.UI
//
// Pantalla de CU-01 (Calcular plan de pasajes).
//
// PATRON FACADE: esta pantalla es el "cliente" de la fachada. Ya no
// conoce a GestorAtleta, GestorMarcas ni MotorPasajes: todo lo pide a
// FachadaPasajes, que coordina esas clases por dentro. La pantalla solo
// junta los datos, muestra el resultado y pide guardar.

// Proyecto: NatSys.UI
//
// Pantalla de CU-01 (Calcular plan de pasajes).
//
// PATRON FACADE: esta pantalla es el "cliente" de la fachada. Ya no
// conoce a GestorAtleta, GestorMarcas ni MotorPasajes: todo lo pide a
// FachadaPasajes, que coordina esas clases por dentro. La pantalla solo
// junta los datos, muestra el resultado y pide guardar.

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmCalcularPasajes : Form
    {
        // El unico punto de entrada a la logica de pasajes
        private readonly FachadaPasajes _fachada = new FachadaPasajes();

        // El plan se guarda a nombre del entrenador que inicio sesion
        private readonly Entrenador _entrenador;

        private List<Atleta> _atletas = new List<Atleta>();
        private List<Prueba> _pruebasDelAtleta = new List<Prueba>();

        // El ultimo calculo, todavia SIN guardar. Es null si no se calculo
        // nada o si se cambio algun dato despues de calcular.
        private ResultadoCalculoPasajes? _resultado;

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
            _atletas = _fachada.ObtenerAtletasActivos();
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
                _pruebasDelAtleta = _fachada.ObtenerPruebasDeAtleta(atleta.IdPersona);
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

            string marcaObjetivo = txtMarcaObjetivo.Text.Trim();
            int largoPileta = int.Parse((string)cmbLargoPileta.SelectedItem!);

            // Paso 15 del diagrama (validar formato en la pantalla): asi
            // el cursor vuelve al campo para corregirlo (CA-01)
            if (!_fachada.ValidarFormatoTiempo(marcaObjetivo))
            {
                MessageBox.Show(_fachada.MensajeFormatoTiempo, "Formato inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMarcaObjetivo.Focus();
                return;
            }

            try
            {
                // Una sola llamada: la fachada calcula el plan, busca el RP
                // y evalua la viabilidad
                _resultado = _fachada.CalcularPlan(atleta, prueba, _entrenador, largoPileta, marcaObjetivo);
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
            dgvPasajes.DataSource = _resultado.Filas;
            MostrarComparativaRP(_resultado);
            btnGuardar.Enabled = true;
            btnExportarPdf.Enabled = true;
        }

        // CU-01 paso 11 + EXT-01 (advertir marca poco realista)
        private void MostrarComparativaRP(ResultadoCalculoPasajes resultado)
        {
            // CA-02: sin marcas previas (SV) el plan se muestra igual, pero
            // sin comparativa
            if (!resultado.TieneRecordPersonal)
            {
                lblRecordPersonal.Text = $"Récord personal en pileta de {resultado.LargoPileta} m: SV (sin marcas previas)";
                lblViabilidad.Text = "";
                return;
            }

            lblRecordPersonal.Text =
                $"Récord personal en pileta de {resultado.LargoPileta} m: {resultado.TiempoRecordPersonal} " +
                $"({resultado.FechaRecordPersonal:dd/MM/yyyy})";

            // Diferencia positiva = el objetivo es mas rapido que el RP
            decimal diferencia = resultado.DiferenciaConRP;
            string segundos = Math.Abs(diferencia).ToString("0.00", CultureInfo.InvariantCulture);
            string comparacion;
            if (diferencia > 0)
                comparacion = $"{segundos} s más rápido que su RP";
            else if (diferencia < 0)
                comparacion = $"{segundos} s más lento que su RP";
            else
                comparacion = "igual a su RP";

            lblViabilidad.Text = $"Viabilidad: {resultado.Viabilidad} (objetivo {comparacion})";

            // Color segun el resultado: verde, naranja o rojo
            if (resultado.EsPocoRealista)
                lblViabilidad.ForeColor = Color.Firebrick;
            else if (resultado.Viabilidad == "Ambiciosa")
                lblViabilidad.ForeColor = Color.DarkOrange;
            else
                lblViabilidad.ForeColor = Color.ForestGreen;

            // EXT-01 (FA-01 de RF-01): advertencia. El plan igual se calculo.
            if (resultado.EsPocoRealista)
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

            var plan = _fachada.ObtenerPlanVigente(atleta.IdPersona, prueba.IdPrueba);

            if (plan == null)
                lblPlanVigente.Text = "Plan vigente: no tiene ninguno guardado para esta prueba.";
            else
                lblPlanVigente.Text =
                    $"Plan vigente: objetivo {_fachada.FormatearTiempo(plan.MarcaObjetivo)} " +
                    $"en pileta de {plan.LargoPileta} m ({plan.Fecha:dd/MM/yyyy}).";
        }

        // CU-01 paso 12 + EXT-02 (pasos 28 a 38 del diagrama de secuencias)
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (_resultado == null) return;

            var plan = _resultado.Plan;

            // Si ya hay un plan vigente, avisamos que va a quedar reemplazado
            var planVigente = _fachada.ObtenerPlanVigente(plan.IdAtleta, plan.IdPrueba);
            if (planVigente != null)
            {
                var respuesta = MessageBox.Show(
                    $"Este atleta ya tiene un plan vigente para esta prueba (objetivo {_fachada.FormatearTiempo(planVigente.MarcaObjetivo)}).\n" +
                    "Si guardás el nuevo, el anterior queda como \"Reemplazado\" en el historial.\n\n¿Guardar igual?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes) return;
            }

            _fachada.GuardarPlan(plan);
            MessageBox.Show("Plan guardado.", "Listo");

            btnGuardar.Enabled = false; // evita guardar dos veces el mismo plan
            MostrarPlanVigente();
        }

        // RF-01: el plan queda "disponible para su consulta y exportacion".
        // Como los atletas no entran al sistema, asi se les pasa su plan.
        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            var resultado = _resultado;
            var atleta = AtletaSeleccionado();
            var prueba = PruebaSeleccionada();
            if (resultado == null || atleta == null || prueba == null) return;

            string nombreAtleta = atleta.GetNombreCompleto();
            string marcaObjetivo = _fachada.FormatearTiempo(resultado.Plan.MarcaObjetivo);

            ExportadorPdf.GuardarConDialogo(
                $"Plan {nombreAtleta} {prueba.Nombre}",
                ruta => ExportadorPdf.ExportarPlanPasajes(ruta, nombreAtleta, prueba.Nombre,
                    _entrenador.GetNombreCompleto(), marcaObjetivo, resultado));
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

        // Borra el calculo y todo lo que se mostraba de el
        private void LimpiarResultado()
        {
            _resultado = null;
            dgvPasajes.DataSource = null;
            lblRecordPersonal.Text = "";
            lblViabilidad.Text = "";
            btnGuardar.Enabled = false;
            btnExportarPdf.Enabled = false;
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
