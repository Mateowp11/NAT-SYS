// Proyecto: NatSys.UI

using NatSys.BLL;

namespace NatSys.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                var sesion = GestorSeguridad.GetInstancia().IniciarSesion(txtUsuario.Text, txtClave.Text);

                var frmMenu = new frmMenuPrincipal(sesion);
                frmMenu.Show();
                this.Hide();
            }
            catch (CuentaBloqueadaException ex)
            {
                MessageBox.Show(ex.Message, "Cuenta bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CredencialesInvalidasException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnOlvideClave_Click(object sender, EventArgs e)
        {
            var frm = new frmRecuperarClave();
            frm.ShowDialog();
        }
    }
}