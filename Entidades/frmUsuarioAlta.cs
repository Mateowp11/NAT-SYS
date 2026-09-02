// Proyecto: NatSys.UI

// Proyecto: NatSys.UI
//
// Decision de producto: los Atletas no tienen acceso directo al sistema
// (si el entrenador quiere compartirles informacion, la exporta en algun
// formato desde Reportes). Por eso esta pantalla solo crea usuarios para
// Entrenadores.

using NatSys.BLL;

namespace NatSys.UI
{
    public partial class frmUsuarioAlta : Form
    {
        public frmUsuarioAlta()
        {
            InitializeComponent();
            cmbPregunta.DataSource = PreguntasSeguridad.Disponibles;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GestorUsuarios.GetInstancia().AgregarUsuarioEntrenador(
                    txtNombre.Text,
                    txtApellido.Text,
                    txtEspecialidad.Text,
                    txtNombreUsuario.Text,
                    txtClaveInicial.Text,
                    cmbPregunta.Text,
                    txtRespuesta.Text);

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