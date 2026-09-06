// Proyecto: NatSys.UI

using NatSys.BLL;

namespace NatSys.UI
{
    public partial class frmGrupoAlta : Form
    {
        public frmGrupoAlta()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GestorGrupos.GetInstancia().AgregarGrupo(txtNombre.Text, txtDescripcion.Text);
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