// Proyecto: NatSys.UI

using NatSys.BLL;
using System.Linq;

namespace NatSys.UI
{
    public partial class frmTorneos : Form
    {
        public frmTorneos()
        {
            InitializeComponent();
            CargarTorneos();
        }

        private void CargarTorneos()
        {
            var torneos = GestorTorneos.GetInstancia().ObtenerTodos();

            var filas = torneos.Select(t => new
            {
                Id = t.IdTorneo,
                Nombre = t.NombreTorneo,
                Fecha = t.Fecha.ToShortDateString(),
                t.Sede,
                t.LargoPileta,
                t.Estado
            }).ToList();

            dgvTorneos.DataSource = filas;

            if (dgvTorneos.Columns["Id"] != null)
                dgvTorneos.Columns["Id"].Visible = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frmAlta = new frmTorneoAlta();
            if (frmAlta.ShowDialog() == DialogResult.OK)
            {
                CargarTorneos();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvTorneos.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un torneo de la lista.", "Aviso");
                return;
            }

            int idTorneo = (int)dgvTorneos.CurrentRow.Cells["Id"].Value;
            var torneo = GestorTorneos.GetInstancia().ObtenerPorId(idTorneo);

            var frmEditar = new frmTorneoAlta(torneo);
            if (frmEditar.ShowDialog() == DialogResult.OK)
            {
                CargarTorneos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTorneos.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un torneo de la lista.", "Aviso");
                return;
            }

            int idTorneo = (int)dgvTorneos.CurrentRow.Cells["Id"].Value;

            var confirmacion = MessageBox.Show(
                "¿Seguro que querés eliminar este torneo?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                GestorTorneos.GetInstancia().EliminarTorneo(idTorneo);
                MessageBox.Show("Torneo eliminado.", "Listo");
                CargarTorneos();
            }
            catch (InvalidOperationException ex)
            {
                // RF-04: tiene marcas asociadas - se ofrece finalizar en su lugar
                var finalizar = MessageBox.Show(
                    ex.Message + "\n\n¿Querés marcarlo como finalizado en su lugar?",
                    "No se puede eliminar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (finalizar == DialogResult.Yes)
                {
                    GestorTorneos.GetInstancia().FinalizarTorneo(idTorneo);
                    MessageBox.Show("Torneo marcado como finalizado.", "Listo");
                    CargarTorneos();
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarTorneos();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}