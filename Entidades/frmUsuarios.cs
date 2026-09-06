// Proyecto: NatSys.UI

using System.Linq;
using NatSys.BLL;

namespace NatSys.UI
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            var usuarios = GestorUsuarios.GetInstancia().ObtenerTodos();

            var filas = usuarios.Select(u => new
            {
                Id = u.IdUsuario,
                u.NombreUsuario,
                Nombre = u.Persona?.GetNombreCompleto(),
                u.Estado
            }).ToList();

            dgvUsuarios.DataSource = filas;

            if (dgvUsuarios.Columns["Id"] != null)
                dgvUsuarios.Columns["Id"].Visible = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frmAlta = new frmUsuarioAlta();
            if (frmAlta.ShowDialog() == DialogResult.OK)
            {
                CargarUsuarios();
            }
        }

        private void btnAsignarGrupo_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un usuario de la lista.", "Aviso");
                return;
            }

            int idUsuario = (int)dgvUsuarios.CurrentRow.Cells["Id"].Value;
            string nombreUsuario = (string)dgvUsuarios.CurrentRow.Cells["NombreUsuario"].Value;

            var frmGrupo = new frmAsignarGrupoUsuario(idUsuario, nombreUsuario);
            frmGrupo.ShowDialog();
        }

        private void btnResetearClave_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un usuario de la lista.", "Aviso");
                return;
            }

            int idUsuario = (int)dgvUsuarios.CurrentRow.Cells["Id"].Value;

            var frmReset = new frmResetearClave(idUsuario);
            frmReset.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un usuario de la lista.", "Aviso");
                return;
            }

            int idUsuario = (int)dgvUsuarios.CurrentRow.Cells["Id"].Value;

            var confirmacion = MessageBox.Show(
                "¿Seguro que querés eliminar este usuario?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            GestorUsuarios.GetInstancia().EliminarUsuario(idUsuario);
            MessageBox.Show("Usuario eliminado.", "Listo");
            CargarUsuarios();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}