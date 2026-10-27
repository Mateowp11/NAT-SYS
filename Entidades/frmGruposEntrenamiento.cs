// Proyecto: NatSys.UI
// Iteracion 2 - CU-06 Gestionar grupos de entrenamiento (prototipo "Gestor de grupos")

using NatSys.BLL;
using System.Linq;

namespace NatSys.UI
{
    public partial class frmGruposEntrenamiento : Form
    {
        public frmGruposEntrenamiento()
        {
            InitializeComponent();
            CargarGrupos();
        }

        // CU-06 paso 2: listado con entrenador y cantidad de atletas
        private void CargarGrupos()
        {
            var grupos = GestorGrupoEntrenamiento.GetInstancia().ObtenerTodos();

            var filas = grupos.Select(g => new
            {
                Id = g.IdGrupoEntrenamiento,
                g.Nombre,
                Entrenador = g.Entrenador?.GetNombreCompleto() ?? "",
                Atletas = g.Atletas.Count,
                g.Estado
            }).ToList();

            dgvGrupos.DataSource = filas;

            if (dgvGrupos.Columns["Id"] != null)
                dgvGrupos.Columns["Id"].Visible = false;
        }

        // Devuelve el Id del grupo elegido en la grilla, o null si no hay ninguno
        private int? GrupoSeleccionado()
        {
            if (dgvGrupos.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un grupo de la lista.", "Aviso");
                return null;
            }
            return (int)dgvGrupos.CurrentRow.Cells["Id"].Value;
        }

        // CU-06 Nuevo (pasos 4 a 8)
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frmAlta = new frmGrupoEntrenamientoAlta();
            if (frmAlta.ShowDialog() == DialogResult.OK)
                CargarGrupos();
        }

        // CU-06 Modificar (pasos 9 a 12)
        private void btnModificar_Click(object sender, EventArgs e)
        {
            int? idGrupo = GrupoSeleccionado();
            if (idGrupo == null) return;

            var grupo = GestorGrupoEntrenamiento.GetInstancia().ObtenerPorId(idGrupo.Value);
            var frmEditar = new frmGrupoEntrenamientoAlta(grupo);
            if (frmEditar.ShowDialog() == DialogResult.OK)
                CargarGrupos();
        }

        // CU-06 Eliminar (pasos 13 a 16)
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int? idGrupo = GrupoSeleccionado();
            if (idGrupo == null) return;

            var gestor = GestorGrupoEntrenamiento.GetInstancia();

            // Paso 14 / CA-02: primero se verifica, despues se pregunta
            if (gestor.TieneTrabajosAsociados(idGrupo.Value))
            {
                MessageBox.Show(
                    "No se puede eliminar el grupo porque tiene trabajos diarios cargados.",
                    "No se puede eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                "¿Seguro que querés eliminar este grupo? Los atletas no se borran, solo dejan de pertenecer a él.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes) return;

            try
            {
                gestor.EliminarGrupo(idGrupo.Value);
                MessageBox.Show("Grupo eliminado.", "Listo");
                CargarGrupos();
            }
            catch (InvalidOperationException ex)
            {
                // Por si alguien le cargo un trabajo entre la verificacion y la confirmacion
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
    }
}
