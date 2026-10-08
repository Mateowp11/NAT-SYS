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

            AplicarPermisosPorRol();
        }

        // Restriccion por tipo de Persona (Atleta no gestiona nada) MAS
        // el permiso especifico para Usuarios/Grupos - eso ya no depende
        // de ser Entrenador, sino de pertenecer al grupo Administradores.
        private void AplicarPermisosPorRol()
        {
            bool esEntrenador = _sesion.Usuario?.Persona is Entrenador;

            btnGestorTorneos.Enabled = esEntrenador;
            btnGestorAtletas.Enabled = esEntrenador;
            btnGestorMarcas.Enabled = esEntrenador;
            btnCalcularPasajes.Enabled = esEntrenador;

            btnGestorUsuarios.Enabled = esEntrenador
                && GestorSeguridad.GetInstancia().VerificarPermiso(_sesion.Usuario, "GESTIONAR_USUARIOS");

            btnGestorGrupos.Enabled = esEntrenador
                && GestorSeguridad.GetInstancia().VerificarPermiso(_sesion.Usuario, "GESTIONAR_GRUPOS");
            // btnReportes queda habilitado para todos: un atleta deberia
            // poder ver su propia evolucion cuando armemos esa pantalla.
        }

        // Abre la gestion de atletas
        private void btnGestorAtletas_Click(object sender, EventArgs e)
        {
            var frm = new frmAtletas();
            frm.ShowDialog();
        }

        private void btnGestorTorneos_Click(object sender, EventArgs e)
        {
            var frm = new frmTorneos();
            frm.ShowDialog();
        }

        private void btnGestorMarcas_Click(object sender, EventArgs e)
        {
            var frm = new frmMarcas();
            frm.ShowDialog();
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

        private void btnGestorUsuarios_Click(object sender, EventArgs e)
        {
            var frm = new frmUsuarios();
            frm.ShowDialog();
        }

        private void btnGestorGrupos_Click(object sender, EventArgs e)
        {
            var frm = new frmGrupos();
            frm.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            GestorSeguridad.GetInstancia().CerrarSesion(_sesion);
            Application.Exit();
        }
    }
}