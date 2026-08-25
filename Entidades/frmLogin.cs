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

                // TODO: reemplazar este mensaje por la apertura real del
                // menu principal cuando lo armemos: FrmMenuPrincipal(sesion)
                MessageBox.Show(
                    $"Sesión iniciada correctamente.\nId de sesión: {sesion.IdSesion}",
                    "Bienvenido/a",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
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
            // TODO: abrir FrmRecuperarClave (usa
            // GestorSeguridad.ObtenerPreguntaSeguridad / RecuperarContraseña)
            // cuando armemos esa pantalla.
            MessageBox.Show("Esta función se habilita en el próximo paso.", "Próximamente");
        }
    }
}
