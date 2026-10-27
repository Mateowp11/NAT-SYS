// Proyecto: NatSys.UI
// Iteracion 2 - menu del modulo de entrenamiento
//
// Cada boton se habilita segun los permisos del usuario (los permisos 8 y 9
// que carga la migracion de la iteracion 2), no segun su rol.

using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmMenuEntrenamiento : Form
    {
        public frmMenuEntrenamiento(Sesion sesion)
        {
            InitializeComponent();

            var seguridad = GestorSeguridad.GetInstancia();
            btnGrupos.Enabled = seguridad.VerificarPermiso(sesion.Usuario, "GESTIONAR_GRUPOS_ENTRENAMIENTO");
            btnTrabajos.Enabled = seguridad.VerificarPermiso(sesion.Usuario, "CARGAR_TRABAJOS");
        }

        // CU-06
        private void btnGrupos_Click(object sender, EventArgs e)
        {
            var frm = new frmGruposEntrenamiento();
            frm.ShowDialog();
        }

        // CU-07
        private void btnTrabajos_Click(object sender, EventArgs e)
        {
            var frm = new frmTrabajosDiarios();
            frm.ShowDialog();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
