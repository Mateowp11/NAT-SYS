// Proyecto: NatSys.UI

using NatSys.BLL;
using System.Linq;

namespace NatSys.UI
{
    public partial class frmGrupos : Form
    {
        public frmGrupos()
        {
            InitializeComponent();
            AjustesVentana.Activar(this, abrirMaximizada: true); // tamaño, lugar y maximizado
            CargarGrupos();
        }

        private void CargarGrupos()
        {
            var grupos = GestorGrupos.GetInstancia().ObtenerTodos();

            var filas = grupos.Select(g => new
            {
                Id = g.IdGrupo,
                g.Nombre,
                g.Descripcion,
                g.Estado
            }).ToList();

            dgvGrupos.DataSource = filas;

            if (dgvGrupos.Columns["Id"] != null)
                dgvGrupos.Columns["Id"].Visible = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frmAlta = new frmGrupoAlta();
            if (frmAlta.ShowDialog() == DialogResult.OK)
            {
                CargarGrupos();
            }
        }

        private void btnAsignarPermisos_Click(object sender, EventArgs e)
        {
            if (dgvGrupos.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un grupo de la lista.", "Aviso");
                return;
            }

            int idGrupo = (int)dgvGrupos.CurrentRow.Cells["Id"].Value;
            string nombreGrupo = (string)dgvGrupos.CurrentRow.Cells["Nombre"].Value;

            var frmPermisos = new frmAsignarPermisos(idGrupo, nombreGrupo);
            frmPermisos.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvGrupos.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un grupo de la lista.", "Aviso");
                return;
            }

            int idGrupo = (int)dgvGrupos.CurrentRow.Cells["Id"].Value;

            var confirmacion = MessageBox.Show(
                "¿Seguro que querés eliminar este grupo?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                GestorGrupos.GetInstancia().EliminarGrupo(idGrupo);
                MessageBox.Show("Grupo eliminado.", "Listo");
                CargarGrupos();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "No se puede eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarGrupos();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Doble clic en una fila = Asignar permisos. El encabezado tiene RowIndex -1
        // y se ignora, asi no se dispara al ordenar columnas.
        private void dgvGrupos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                btnAsignarPermisos.PerformClick();
        }
    }
}