// Proyecto: NatSys.UI

using NatSys.BLL;


namespace NatSys.UI
{
    public partial class frmResetearClave : Form
    {
        private readonly int _idUsuario;

        public frmResetearClave(int idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtClaveNueva.Text != txtConfirmarClave.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Reseteo por el administrador: no pide la clave actual, y
                // de paso desbloquea la cuenta si estaba bloqueada.
                GestorUsuarios.GetInstancia().ResetearClave(_idUsuario, txtClaveNueva.Text);

                MessageBox.Show("Contraseña restablecida.", "Listo");
                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}