// Proyecto: NatSys.UI
//
// Historial de marcas (CU-03, paso 2): lista todas las marcas con filtros
// por atleta, prueba y tipo, y da acceso a Alta, Modificacion, Consulta
// (Ver detalle) y Baja.

using System.Collections.Generic;
using System.Linq;
using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmMarcas : Form
    {
        // Guardamos la lista completa para poder filtrarla sin volver a
        // consultar la base cada vez que se cambia un filtro.
        private List<Marca>? _todasLasMarcas;

        public frmMarcas()
        {
            InitializeComponent();
            CargarFiltros();
            CargarMarcas();
        }

        // El primer elemento de cada combo es "Todos/Todas", que significa
        // "no filtrar por esto".
        private void CargarFiltros()
        {
            cmbFiltroAtleta.Items.Add(new OpcionCombo(0, "Todos"));
            foreach (var atleta in GestorAtleta.GetInstancia().ObtenerTodos())
                cmbFiltroAtleta.Items.Add(new OpcionCombo(atleta.IdPersona, atleta.GetNombreCompleto()));
            cmbFiltroAtleta.SelectedIndex = 0;

            cmbFiltroPrueba.Items.Add(new OpcionCombo(0, "Todas"));
            foreach (var prueba in GestorPruebas.GetInstancia().ObtenerTodas())
                cmbFiltroPrueba.Items.Add(new OpcionCombo(prueba.IdPrueba, prueba.Nombre));
            cmbFiltroPrueba.SelectedIndex = 0;

            cmbFiltroTipo.SelectedIndex = 0; // "Todos" (las opciones estan en el Designer)
        }

        private void CargarMarcas()
        {
            _todasLasMarcas = GestorMarcas.GetInstancia().ObtenerTodas();
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            // Mientras se cargan los combos todavia no hay marcas: no hacer nada
            if (_todasLasMarcas == null) return;

            IEnumerable<Marca> filtradas = _todasLasMarcas;

            // Cada filtro solo se aplica si NO esta elegida la primera opcion
            if (cmbFiltroAtleta.SelectedIndex > 0)
            {
                int idAtleta = ((OpcionCombo)cmbFiltroAtleta.SelectedItem!).Id;
                filtradas = filtradas.Where(m => m.IdAtleta == idAtleta);
            }

            if (cmbFiltroPrueba.SelectedIndex > 0)
            {
                int idPrueba = ((OpcionCombo)cmbFiltroPrueba.SelectedItem!).Id;
                filtradas = filtradas.Where(m => m.IdPrueba == idPrueba);
            }

            if (cmbFiltroTipo.SelectedIndex > 0)
            {
                string tipo = (string)cmbFiltroTipo.SelectedItem!;
                filtradas = filtradas.Where(m => m.Tipo == tipo);
            }

            var motor = MotorPasajes.GetInstancia();

            dgvMarcas.DataSource = filtradas.Select(m => new
            {
                Id = m.IdMarca,
                Fecha = m.Fecha.ToShortDateString(),
                Atleta = m.Atleta?.GetNombreCompleto(),
                Prueba = m.Prueba?.Nombre,
                Pileta = m.LargoPileta + " m",
                Tiempo = motor.FormatearParaMostrar(m.Tiempo), // "28.50" en vez de "00:28.50"
                m.Tipo,
                Torneo = m.TorneoRegistro?.NombreTorneo ?? "-",
                RP = m.EsRecordPersonal ? "Sí" : ""
            }).ToList();

            // El Id lo necesitamos para saber que marca se eligio, pero el
            // usuario no tiene por que verlo
            var columnaId = dgvMarcas.Columns["Id"];
            if (columnaId != null)
                columnaId.Visible = false;
        }

        // Un solo metodo atiende los 3 combos de filtro (se conecta a los
        // tres en el Designer)
        private void Filtro_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        // Devuelve el Id de la marca seleccionada en la grilla, o null si no
        // hay ninguna (y en ese caso avisa al usuario).
        private int? IdMarcaSeleccionada()
        {
            if (dgvMarcas.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná una marca de la lista.", "Aviso");
                return null;
            }
            return (int)dgvMarcas.CurrentRow.Cells["Id"].Value;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frmAlta = new frmMarcaAlta();
            if (frmAlta.ShowDialog() == DialogResult.OK)
                CargarMarcas();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            AbrirMarcaSeleccionada(soloLectura: false);
        }

        // CU-03, Consulta: muestra la marca en modo solo lectura
        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            AbrirMarcaSeleccionada(soloLectura: true);
        }

        private void AbrirMarcaSeleccionada(bool soloLectura)
        {
            int? idMarca = IdMarcaSeleccionada();
            if (idMarca == null) return;

            var marca = GestorMarcas.GetInstancia().ObtenerPorId(idMarca.Value);
            if (marca == null)
            {
                MessageBox.Show("La marca ya no existe. Actualizá la lista.", "Aviso");
                return;
            }

            var frm = new frmMarcaAlta(marca, soloLectura);
            if (frm.ShowDialog() == DialogResult.OK)
                CargarMarcas();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int? idMarca = IdMarcaSeleccionada();
            if (idMarca == null) return;

            var confirmacion = MessageBox.Show(
                "¿Seguro que querés eliminar esta marca?\nSi era el récord personal, se recalcula automáticamente.",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                GestorMarcas.GetInstancia().EliminarMarca(idMarca.Value);
                MessageBox.Show("Marca eliminada.", "Listo");
                CargarMarcas();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarMarcas();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}