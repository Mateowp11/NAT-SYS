// Proyecto: NatSys.UI
//
// Formulario de UNA marca (CU-03). El mismo formulario sirve para tres
// operaciones, segun con que constructor se lo abra:
//   - Alta:          new frmMarcaAlta()
//   - Modificacion:  new frmMarcaAlta(marca, soloLectura: false)
//   - Consulta:      new frmMarcaAlta(marca, soloLectura: true)   ("Ver detalle")

using System.Collections.Generic;
using System.Linq;
using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmMarcaAlta : Form
    {
        // null = estamos dando de alta una marca nueva
        private readonly Marca? _marcaAEditar;

        // Guardamos los torneos cargados en el combo para poder leer su
        // largo de pileta y su fecha cuando el entrenador elige uno.
        private List<Torneo> _torneos = new List<Torneo>();

        // true mientras el constructor llena los controles. Evita que
        // cmbTorneo_SelectedIndexChanged pise la pileta y la fecha guardadas
        // al abrir una marca existente.
        private bool _cargando = true;

        // ALTA
        public frmMarcaAlta()
        {
            InitializeComponent();
            _marcaAEditar = null;
            this.Text = "Nueva marca";

            CargarCombos(esAlta: true);
            cmbTipo.SelectedIndex = 0;          // "oficial"
            cmbLargoPileta.SelectedIndex = 0;   // "25"
            dtpFecha.Value = DateTime.Today;
            lblRecordPersonal.Visible = false;  // el RP lo calcula el Gestor al guardar

            _cargando = false;
            ActualizarEstadoTorneo();
        }

        // MODIFICACION o CONSULTA
        public frmMarcaAlta(Marca marca, bool soloLectura)
        {
            InitializeComponent();
            _marcaAEditar = marca;
            this.Text = soloLectura ? "Detalle de marca" : "Modificar marca";

            CargarCombos(esAlta: false);

            // Llenamos cada control con los datos actuales de la marca
            OpcionCombo.Seleccionar(cmbAtleta, marca.IdAtleta);
            cmbTipo.SelectedItem = marca.Tipo;
            OpcionCombo.Seleccionar(cmbPrueba, marca.IdPrueba);
            cmbLargoPileta.SelectedItem = marca.LargoPileta.ToString();
            txtTiempo.Text = MotorPasajes.GetInstancia().FormatearParaMostrar(marca.Tiempo);
            OpcionCombo.Seleccionar(cmbTorneo, marca.IdTorneo);
            dtpFecha.Value = marca.Fecha;
            lblRecordPersonal.Text = marca.EsRecordPersonal
                ? "Récord personal: Sí"
                : "Récord personal: No";

            // Una marca no cambia de atleta: si se cargo al atleta
            // equivocado, se elimina y se carga de nuevo.
            cmbAtleta.Enabled = false;

            _cargando = false;
            ActualizarEstadoTorneo();

            if (soloLectura)
                BloquearEdicion();
        }

        // Llena los combos de atleta, prueba y torneo.
        // En el ALTA solo se ofrecen atletas activos (CA-02) y torneos
        // activos. En MODIFICACION/CONSULTA se cargan todos, porque una marca
        // vieja puede ser de un atleta que hoy esta inactivo o de un torneo
        // que ya se finalizo, y igual hay que poder mostrarlos.
        private void CargarCombos(bool esAlta)
        {
            var atletas = esAlta
                ? GestorAtleta.GetInstancia().GetAtletasActivos()
                : GestorAtleta.GetInstancia().ObtenerTodos();
            foreach (var atleta in atletas)
                cmbAtleta.Items.Add(new OpcionCombo(atleta.IdPersona, atleta.GetNombreCompleto()));

            foreach (var prueba in GestorPruebas.GetInstancia().ObtenerTodas())
                cmbPrueba.Items.Add(new OpcionCombo(prueba.IdPrueba, prueba.Nombre));

            _torneos = GestorTorneos.GetInstancia().ObtenerTodos();
            if (esAlta)
                _torneos = _torneos.Where(t => t.Estado == "activo").ToList();
            foreach (var torneo in _torneos)
                cmbTorneo.Items.Add(new OpcionCombo(torneo.IdTorneo, $"{torneo.NombreTorneo} ({torneo.Fecha:dd/MM/yyyy})"));
        }

        // CU-03 paso 7: el torneo solo se elige si la marca es oficial.
        // Si es un test, el combo se apaga y se vacia.
        private void ActualizarEstadoTorneo()
        {
            bool esOficial = (string?)cmbTipo.SelectedItem == "oficial";
            cmbTorneo.Enabled = esOficial;
            if (!esOficial)
                cmbTorneo.SelectedIndex = -1;
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEstadoTorneo();
        }

        // RF-04: al elegir un torneo, se proponen su largo de pileta y su
        // fecha. El entrenador despues los puede cambiar a mano.
        private void cmbTorneo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            var torneo = TorneoSeleccionado();
            if (torneo == null) return;

            cmbLargoPileta.SelectedItem = torneo.LargoPileta.ToString();
            dtpFecha.Value = torneo.Fecha;
        }

        // Devuelve el Torneo completo que esta elegido en el combo, o null
        private Torneo? TorneoSeleccionado()
        {
            var opcion = cmbTorneo.SelectedItem as OpcionCombo;
            if (opcion == null) return null;
            return _torneos.FirstOrDefault(t => t.IdTorneo == opcion.Id);
        }

        // CU-03 paso 17 (Consulta): se ve todo pero no se puede tocar nada
        private void BloquearEdicion()
        {
            cmbAtleta.Enabled = false;
            cmbTipo.Enabled = false;
            cmbPrueba.Enabled = false;
            cmbLargoPileta.Enabled = false;
            txtTiempo.ReadOnly = true;
            cmbTorneo.Enabled = false;
            dtpFecha.Enabled = false;

            btnGuardar.Visible = false;
            btnCancelar.Text = "Cerrar";
            this.AcceptButton = btnCancelar; // Enter cierra la ventana
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Esto lo controla la UI porque si el combo esta vacio no hay
            // ningun Id para mandarle al Gestor.
            var atleta = cmbAtleta.SelectedItem as OpcionCombo;
            var prueba = cmbPrueba.SelectedItem as OpcionCombo;
            if (atleta == null || prueba == null)
            {
                MessageBox.Show("Elegí el atleta y la prueba.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tipo = (string)cmbTipo.SelectedItem!;
            int largoPileta = int.Parse((string)cmbLargoPileta.SelectedItem!);
            int? idTorneo = (cmbTorneo.SelectedItem as OpcionCombo)?.Id; // null si no hay torneo
            string tiempo = txtTiempo.Text.Trim();
            DateTime fecha = dtpFecha.Value.Date; // .Date = solo el dia, sin la hora

            try
            {
                // El resto de las reglas (formato del tiempo, oficial con
                // torneo, atleta activo, calculo del RP) las aplica el Gestor.
                if (_marcaAEditar == null)
                {
                    GestorMarcas.GetInstancia().AgregarMarca(
                        atleta.Id, tipo, prueba.Id, largoPileta, tiempo, idTorneo, fecha);
                }
                else
                {
                    GestorMarcas.GetInstancia().ModificarMarca(
                        _marcaAEditar.IdMarca, tipo, prueba.Id, largoPileta, tiempo, idTorneo, fecha);
                }

                MessageBox.Show("Marca guardada.", "Listo");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException ex) // CA-01 (formato) y CA-03 (oficial sin torneo)
            {
                MessageBox.Show(ex.Message, "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex) // CA-02 (atleta inactivo)
            {
                MessageBox.Show(ex.Message, "No se puede registrar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}