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
            bool estaActivo = dgvAtletas.CurrentRow.Cells["Estado"].Value?.ToString() == "activo";

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
                // Si ya esta inactivo, no tiene sentido ofrecer desactivarlo
                if (!estaActivo)
                {
                    MessageBox.Show(ex.Message, "No se puede eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // RF-02: si tiene marcas o planes, no se puede eliminar -
                // se ofrece desactivar en su lugar (baja logica)
                var desactivar = MessageBox.Show(
                    ex.Message + "\n\nSi lo que querés es que deje de aparecer, ¿lo desactivo en su lugar?",
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

        // Baja logica y reactivacion con un solo boton: si el atleta elegido
        // esta activo lo desactiva, y si esta inactivo lo vuelve a activar.
        // En los dos casos se conserva todo su historial (marcas y planes).
        private void btnActivarDesactivar_Click(object sender, EventArgs e)
        {
            if (dgvAtletas.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un atleta de la lista.", "Aviso");
                return;
            }

            int idAtleta = (int)dgvAtletas.CurrentRow.Cells["Id"].Value;
            string nombre = dgvAtletas.CurrentRow.Cells["Nombre"].Value?.ToString() ?? "";
            bool estaActivo = dgvAtletas.CurrentRow.Cells["Estado"].Value?.ToString() == "activo";

            string pregunta = estaActivo
                ? $"¿Desactivar a {nombre}?\n\nNo se le van a poder cargar marcas nuevas ni calcular pasajes, pero se conserva todo su historial."
                : $"¿Volver a activar a {nombre}?";

            var confirmacion = MessageBox.Show(pregunta, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes) return;

            if (estaActivo)
            {
                GestorAtleta.GetInstancia().DesactivarAtleta(idAtleta);
                MessageBox.Show("Atleta desactivado.", "Listo");
            }
            else
            {
                GestorAtleta.GetInstancia().ReactivarAtleta(idAtleta);
                MessageBox.Show("Atleta activado.", "Listo");
            }

            CargarAtletas();
        }

        // Abre los planes de pasajes guardados del atleta elegido, para verlos
        // o borrarlos (por ejemplo, para poder eliminar un atleta cargado por error)
        private void btnPlanes_Click(object sender, EventArgs e)
        {
            if (dgvAtletas.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un atleta de la lista.", "Aviso");
                return;
            }

            int idAtleta = (int)dgvAtletas.CurrentRow.Cells["Id"].Value;
            string nombre = dgvAtletas.CurrentRow.Cells["Nombre"].Value?.ToString() ?? "";

            var frm = new frmPlanesAtleta(idAtleta, nombre);
            frm.ShowDialog();
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