// Proyecto: NatSys.UI
//
// Planes de pasajes guardados de UN atleta.
// Arriba: la lista de planes (vigentes y reemplazados).
// Abajo: los tramos del plan seleccionado.
// Permite borrar un plan, por ejemplo para poder eliminar un atleta que se
// cargo por error (un atleta con planes guardados no se puede eliminar).
//
// Igual que frmCalcularPasajes, entra a la BLL por FachadaPasajes.

using System.Collections.Generic;
using System.Linq;
using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmPlanesAtleta : Form
    {
        private readonly int _idAtleta;
        private readonly FachadaPasajes _fachada = new FachadaPasajes();

        // Se guardan los planes completos para mostrar los tramos del que se
        // elija sin volver a consultar la base
        private List<PlanPasaje> _planes = new List<PlanPasaje>();

        public frmPlanesAtleta(int idAtleta, string nombreAtleta)
        {
            InitializeComponent();
            _idAtleta = idAtleta;
            lblTitulo.Text = $"Planes guardados de {nombreAtleta}";
            CargarPlanes();
        }

        // Recien cuando la ventana se muestra la grilla tiene una fila
        // seleccionada: ahi se cargan los tramos del primer plan
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            MostrarTramos();
        }

        private void CargarPlanes()
        {
            _planes = _fachada.ObtenerPlanesDeAtleta(_idAtleta);

            dgvPlanes.DataSource = _planes.Select(p => new
            {
                Id = p.IdPlanPasaje,
                Fecha = p.Fecha.ToString("dd/MM/yyyy"),
                Prueba = p.Prueba?.Nombre,
                Pileta = p.LargoPileta + " m",
                Objetivo = _fachada.FormatearTiempo(p.MarcaObjetivo),
                // En la base dice "Disponible"; en pantalla es mas claro "Vigente"
                Estado = p.Estado == "Disponible" ? "Vigente" : p.Estado,
                Entrenador = p.Entrenador?.GetNombreCompleto()
            }).ToList();

            if (dgvPlanes.Columns["Id"] != null)
                dgvPlanes.Columns["Id"].Visible = false;

            lblSinPlanes.Visible = _planes.Count == 0;
            btnEliminar.Enabled = _planes.Count > 0;

            MostrarTramos();
        }

        // Cada vez que cambia la fila elegida, se muestran sus tramos abajo
        private void dgvPlanes_SelectionChanged(object sender, EventArgs e)
        {
            MostrarTramos();
        }

        private void MostrarTramos()
        {
            PlanPasaje? plan = PlanSeleccionado();
            dgvTramos.DataSource = plan == null
                ? new List<FilaPasaje>()
                : _fachada.ObtenerFilasDePlan(plan);
        }

        // El plan de la fila elegida (o null si no hay ninguna)
        private PlanPasaje? PlanSeleccionado()
        {
            if (dgvPlanes.CurrentRow == null) return null;

            int idPlan = (int)dgvPlanes.CurrentRow.Cells["Id"].Value;
            return _planes.FirstOrDefault(p => p.IdPlanPasaje == idPlan);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            PlanPasaje? plan = PlanSeleccionado();
            if (plan == null)
            {
                MessageBox.Show("Seleccioná un plan de la lista.", "Aviso");
                return;
            }

            string pregunta =
                $"¿Eliminar el plan de {plan.Prueba?.Nombre} (objetivo {_fachada.FormatearTiempo(plan.MarcaObjetivo)}) " +
                $"del {plan.Fecha:dd/MM/yyyy}?\n\nNo se puede deshacer.";

            // Si es el vigente, avisamos que la prueba se queda sin plan vigente
            if (plan.Estado == "Disponible")
                pregunta += "\nEs el plan vigente: esta prueba se va a quedar sin plan vigente hasta que guardes uno nuevo.";

            var confirmacion = MessageBox.Show(pregunta, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes) return;

            _fachada.EliminarPlan(plan.IdPlanPasaje);
            MessageBox.Show("Plan eliminado.", "Listo");
            CargarPlanes();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}