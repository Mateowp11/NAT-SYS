// Proyecto: NatSys.UI

using NatSys.BLL;

namespace NatSys.UI
{
    public partial class frmRecuperarClave : Form
    {
        public frmRecuperarClave()
        {
            InitializeComponent();
        }

        private void btnBuscarPregunta_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Ingresá tu nombre de usuario.", "Aviso");
                return;
            }

            string pregunta = GestorSeguridad.GetInstancia().ObtenerPreguntaSeguridad(txtUsuario.Text);

            if (pregunta == null)
            {
                MessageBox.Show("No se encontró ese usuario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Recien aca mostramos el segundo paso, con la pregunta ya cargada
            lblPregunta.Text = pregunta;
            lblPregunta.Visible = true;
            lblRespuesta.Visible = true;
            txtRespuesta.Visible = true;
            lblClaveNueva.Visible = true;
            txtClaveNueva.Visible = true;
            lblConfirmarClave.Visible = true;
            txtConfirmarClave.Visible = true;
            btnRestablecer.Visible = true;

            txtUsuario.Enabled = false;
            btnBuscarPregunta.Enabled = false;
        }

        private void btnRestablecer_Click(object sender, EventArgs e)
        {
            if (txtClaveNueva.Text != txtConfirmarClave.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                GestorSeguridad.GetInstancia().RecuperarContraseña(txtUsuario.Text, txtRespuesta.Text, txtClaveNueva.Text);

                MessageBox.Show(
                    "Contraseña actualizada correctamente. Ya podés iniciar sesión.",
                    "Listo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
            catch (CuentaBloqueadaException ex)
            {
                // Fallo la respuesta y ademas se agoto el limite de intentos
                MessageBox.Show(ex.Message, "Cuenta bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            catch (CredencialesInvalidasException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}