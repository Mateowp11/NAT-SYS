// Proyecto: NatSys.UI

using System.Linq;
using NatSys.BLL;

namespace NatSys.UI
{
    public partial class frmAtletas : Form
    {
        public frmAtletas()
        {
            InitializeComponent();
            CargarAtletas();
        }

        private void CargarAtletas()
        {
            var atletas = GestorAtleta.GetInstancia().ObtenerTodos();

            // Proyectamos solo lo que queremos mostrar en la grilla - el
            // DataGridView genera las columnas solo con AutoGenerateColumns
            var filas = atletas.Select(a => new
            {
                Id = a.IdPersona,
                Nombre = a.GetNombreCompleto(),
                a.Categoria,
                a.Estado
            }).ToList();

            dgvAtletas.DataSource = filas;

            if (dgvAtletas.Columns["Id"] != null)
                dgvAtletas.Columns["Id"].Visible = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frmAlta = new frmAtletaAlta();
            if (frmAlta.ShowDialog() == DialogResult.OK)
            {
                CargarAtletas();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvAtletas.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un atleta de la lista.", "Aviso");
                return;
            }

            int idAtleta = (int)dgvAtletas.CurrentRow.Cells["Id"].Value;
            var atleta = GestorAtleta.GetInstancia().ObtenerPorId(idAtleta);

            var frmEditar = new frmAtletaAlta(atleta);
            if (frmEditar.ShowDialog() == DialogResult.OK)
            {
                CargarAtletas();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAtletas.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un atleta de la lista.", "Aviso");
                return;
            }

            int idAtleta = (int)dgvAtletas.CurrentRow.Cells["Id"].Value;

            var confirmacion = MessageBox.Show(
                "¿Seguro que querés eliminar este atleta?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                GestorAtleta.GetInstancia().EliminarAtleta(idAtleta);
                MessageBox.Show("Atleta eliminado.", "Listo");
                CargarAtletas();
            }
            catch (InvalidOperationException ex)
            {
                // RF-02: si tiene marcas asociadas, no se puede eliminar -
                // se ofrece desactivar en su lugar (baja logica)
                var desactivar = MessageBox.Show(
                    ex.Message + "\n\n¿Querés desactivarlo en su lugar?",
                    "No se puede eliminar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (desactivar == DialogResult.Yes)
                {
                    GestorAtleta.GetInstancia().DesactivarAtleta(idAtleta);
                    MessageBox.Show("Atleta desactivado.", "Listo");
                    CargarAtletas();
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarAtletas();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}