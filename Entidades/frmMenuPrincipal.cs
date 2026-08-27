// Proyecto: NatSys.UI

using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmMenuPrincipal : Form
    {
        private readonly Sesion _sesion;

        public frmMenuPrincipal(Sesion sesion)
        {
            InitializeComponent();
            _sesion = sesion;
            lblBienvenida.Text = $"Bienvenido/a, {_sesion.Usuario?.Persona?.GetNombreCompleto()}";
        }

        // TODO: reemplazar por new FrmAtletas().Show() cuando armemos esa pantalla
        private void btnGestorAtletas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta función se habilita en el próximo paso.", "Próximamente");
        }

        // TODO: reemplazar por new FrmTorneos().Show()
        private void btnGestorTorneos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta función se habilita en el próximo paso.", "Próximamente");
        }

        // TODO: reemplazar por new FrmMarcas().Show()
        private void btnGestorMarcas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta función se habilita en el próximo paso.", "Próximamente");
        }

        // TODO: reemplazar por new FrmCalcularPasajes().Show()
        private void btnCalcularPasajes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta función se habilita en el próximo paso.", "Próximamente");
        }

        // TODO: reemplazar por new FrmReportes().Show()
        private void btnReportes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta función se habilita en el próximo paso.", "Próximamente");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            GestorSeguridad.GetInstancia().CerrarSesion(_sesion);
            Application.Exit();
        }
    }
}