// Proyecto: NatSys.UI
//
// Primera pantalla del programa, antes del login. Verifica que se pueda
// llegar a la base y que la base sea de la misma version que el programa.
//
// La verificacion corre en segundo plano (Task.Run) para que la ventana
// no quede "No responde" mientras Azure despierta la base, que puede
// tardar hasta un minuto si hacia rato que nadie la usaba.

using NatSys.BLL;

namespace NatSys.UI
{
    public partial class frmConexion : Form
    {
        public frmConexion()
        {
            InitializeComponent();
        }

        // OnShown: arranca la verificacion recien cuando la ventana ya se ve
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await VerificarAsync();
        }

        private async Task VerificarAsync()
        {
            lblEstado.Text = "Conectando con la base de datos...";

            var resultado = await Task.Run(() => GestorBaseDatos.GetInstancia().Verificar());

            if (resultado.Correcto)
            {
                DialogResult = DialogResult.OK; // Program.cs sigue con el login
                Close();
                return;
            }

            var botones = resultado.SePuedeReintentar ? MessageBoxButtons.RetryCancel : MessageBoxButtons.OK;
            var respuesta = MessageBox.Show(this, resultado.Mensaje, "NAT-SYS", botones, MessageBoxIcon.Warning);

            if (respuesta == DialogResult.Retry)
            {
                await VerificarAsync();
                return;
            }

            DialogResult = DialogResult.Cancel; // Program.cs cierra el programa
            Close();
        }
    }
}