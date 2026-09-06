// Proyecto: NatSys.UI
//
// Sirve para ALTA y MODIFICAR, igual que frmAtletaAlta: constructor vacio
// = torneo nuevo; constructor con un Torneo existente = edicion.

using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmTorneoAlta : Form
    {
        private readonly Torneo? _torneoAEditar;

        public frmTorneoAlta()
        {
            InitializeComponent();
            _torneoAEditar = null;
            this.Text = "Nuevo torneo";
            cmbLargoPileta.SelectedIndex = 0;
        }

        public frmTorneoAlta(Torneo torneo)
        {
            InitializeComponent();
            _torneoAEditar = torneo;
            this.Text = "Modificar torneo";

            txtNombre.Text = torneo.NombreTorneo;
            dtpFecha.Value = torneo.Fecha;
            txtSede.Text = torneo.Sede;
            cmbLargoPileta.SelectedItem = torneo.LargoPileta.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int largoPileta = int.Parse((string)cmbLargoPileta.SelectedItem);

                if (_torneoAEditar == null)
                {
                    GestorTorneos.GetInstancia().AgregarTorneo(
                        txtNombre.Text, dtpFecha.Value, txtSede.Text, largoPileta);
                }
                else
                {
                    GestorTorneos.GetInstancia().ModificarTorneo(
                        _torneoAEditar.IdTorneo, txtNombre.Text, dtpFecha.Value, txtSede.Text, largoPileta);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}