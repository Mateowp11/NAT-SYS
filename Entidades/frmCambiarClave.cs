// Proyecto: NatSys.UI
//
// RF-05.5: cambiar la contraseña propia. Lo usa cualquier usuario que haya
// iniciado sesion (se abre desde el menu principal). Las reglas (clave
// actual correcta, distinta de la anterior, 8 caracteres con letras y
// numeros) estan en GestorSeguridad; aca solo se piden los datos.

using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmCambiarClave : Form
    {
        private readonly Usuario _usuario;

        public frmCambiarClave(Usuario usuario)
        {
            InitializeComponent();
            AjustesVentana.Activar(this, abrirMaximizada: false);
            _usuario = usuario;
            Text = $"NAT-SYS - Cambiar contraseña ({usuario.NombreUsuario})";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtClaveNueva.Text != txtConfirmarClave.Text)
            {
                MessageBox.Show("La contraseña nueva y su repetición no coinciden.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarClave.Focus();
                return;
            }

            try
            {
                GestorSeguridad.GetInstancia().CambiarContraseña(_usuario, txtClaveActual.Text, txtClaveNueva.Text);

                MessageBox.Show("Contraseña cambiada. La próxima vez que inicies sesión usá la nueva.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (CredencialesInvalidasException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Muestra u oculta lo que se escribe en los tres campos
        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            bool ocultar = !chkMostrar.Checked;
            txtClaveActual.UseSystemPasswordChar = ocultar;
            txtClaveNueva.UseSystemPasswordChar = ocultar;
            txtConfirmarClave.UseSystemPasswordChar = ocultar;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
