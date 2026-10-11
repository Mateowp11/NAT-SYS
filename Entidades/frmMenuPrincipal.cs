// Proyecto: NatSys.UI
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

        // CU-01: el plan se guarda a nombre del entrenador que inicio sesion,
        // por eso se lo pasamos a la pantalla.
        private void btnCalcularPasajes_Click(object sender, EventArgs e)
        {
            var entrenador = _sesion.Usuario?.Persona as Entrenador;
            if (entrenador == null) return; // el boton ya esta deshabilitado para quien no es entrenador

            var frm = new frmCalcularPasajes(entrenador);
            frm.ShowDialog();
        }

        // RF-06: menu con los dos reportes
        private void btnReportes_Click(object sender, EventArgs e)
        {
            var frm = new frmReportes();
            frm.ShowDialog();
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