// Proyecto: NatSys.UI
// Iteracion 2 - CU-07 Cargar trabajo diario (prototipo "Cargar trabajo")

using NatSys.BLL;
using NatSys.Entidades;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace NatSys.UI
{
    public partial class frmTrabajosDiarios : Form
    {
        // Los trabajos del grupo elegido, tal como los devolvio el gestor
        // (sin los bytes del Excel: esos se piden solo al abrirlo)
        private List<TrabajoDiario> _trabajos = new List<TrabajoDiario>();

        public frmTrabajosDiarios()
        {
            InitializeComponent();
        }

        // CU-07 paso 2. Precondicion del caso de uso: tiene que existir al
        // menos un grupo de entrenamiento.
        private void frmTrabajosDiarios_Load(object sender, EventArgs e)
        {
            var grupos = GestorGrupoEntrenamiento.GetInstancia().ObtenerTodos();

            cmbGrupo.Items.Clear();
            foreach (var grupo in grupos)
                cmbGrupo.Items.Add(new OpcionCombo(grupo.IdGrupoEntrenamiento, grupo.Nombre));

            if (cmbGrupo.Items.Count == 0)
            {
                btnAgregar.Enabled = false;
                MessageBox.Show(
                    "Todavía no hay grupos de entrenamiento. Creá uno desde \"Grupos de entrenamiento\" antes de cargar trabajos.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cmbGrupo.SelectedIndex = 0; // esto dispara CargarTrabajos()
        }

        private void cmbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTrabajos();
        }

        // La casilla de la fecha funciona como filtro: tildada muestra solo
        // ese dia; destildada, todos los trabajos del grupo
        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            CargarTrabajos();
        }

        // CU-07 paso 3
        private void CargarTrabajos()
        {
            if (cmbGrupo.SelectedItem is not OpcionCombo grupo)
            {
                _trabajos = new List<TrabajoDiario>();
                dgvTrabajos.DataSource = null;
                return;
            }

            _trabajos = GestorTrabajoDiario.GetInstancia().ObtenerTrabajosPorGrupo(grupo.Id);

            var visibles = dtpFecha.Checked
                ? _trabajos.Where(t => t.Fecha.Date == dtpFecha.Value.Date).ToList()
                : _trabajos;

            var filas = visibles.Select(t => new
            {
                Id = t.IdTrabajo,
                Fecha = t.Fecha.ToShortDateString(),
                t.Tipo,
                SeriePrincipal = DescribirSerie(t),
                Archivo = t.TieneArchivo() ? t.ArchivoNombre : "-"
            }).ToList();

            dgvTrabajos.DataSource = filas;

            if (dgvTrabajos.Columns["Id"] != null)
                dgvTrabajos.Columns["Id"].Visible = false;
            if (dgvTrabajos.Columns["SeriePrincipal"] != null)
                dgvTrabajos.Columns["SeriePrincipal"].HeaderText = "Serie principal";
        }

        // La columna "Serie principal" del prototipo: "6x100 VO2 + 8x50 Mariposa"
        private static string DescribirSerie(TrabajoDiario trabajo)
        {
            if (!trabajo.Bloques.Any())
                return "Sin serie";

            return string.Join(" + ", trabajo.Bloques
                .OrderBy(b => b.Orden)
                .Select(b => $"{b.Repeticiones}x{b.Distancia} {b.Descripcion}"));
        }

        private TrabajoDiario? TrabajoSeleccionado()
        {
            if (dgvTrabajos.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un trabajo de la lista.", "Aviso");
                return null;
            }

            int idTrabajo = (int)dgvTrabajos.CurrentRow.Cells["Id"].Value;
            return _trabajos.First(t => t.IdTrabajo == idTrabajo);
        }

        // CU-07 Agregar (pasos 5 a 11)
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbGrupo.SelectedItem is not OpcionCombo grupo)
            {
                MessageBox.Show("Seleccioná un grupo.", "Aviso");
                return;
            }

            // Si se esta mirando un dia en particular, el trabajo nuevo se propone para ese dia
            DateTime fechaPropuesta = dtpFecha.Checked ? dtpFecha.Value.Date : DateTime.Today;

            var frmAlta = new frmTrabajoDiarioAlta(grupo.Id, grupo.Texto, fechaPropuesta);
            if (frmAlta.ShowDialog() == DialogResult.OK)
                CargarTrabajos();
        }

        // CU-07 Abrir (pasos 12-13). Los bytes se guardan como archivo en la
        // carpeta temporal de Windows y se abren con el programa asociado (Excel).
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            var trabajo = TrabajoSeleccionado();
            if (trabajo == null) return;

            try
            {
                byte[] archivo = GestorTrabajoDiario.GetInstancia().ObtenerArchivo(trabajo.IdTrabajo);

                string carpeta = Path.Combine(Path.GetTempPath(), "NatSys");
                Directory.CreateDirectory(carpeta);
                string ruta = Path.Combine(carpeta, $"{trabajo.IdTrabajo}_{trabajo.ArchivoNombre}");

                File.WriteAllBytes(ruta, archivo);
                Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });
            }
            catch (InvalidOperationException ex)
            {
                // CA-02: el trabajo no tiene archivo
                MessageBox.Show(ex.Message, "Sin archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException)
            {
                MessageBox.Show("El archivo ya está abierto. Cerralo en Excel y volvé a intentar.",
                    "No se pudo abrir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("No hay ningún programa instalado para abrir archivos de Excel.",
                    "No se pudo abrir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // CU-07 Eliminar (pasos 14 a 17) y CA-03
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var trabajo = TrabajoSeleccionado();
            if (trabajo == null) return;

            var gestor = GestorTrabajoDiario.GetInstancia();

            string mensaje;
            MessageBoxIcon icono;
            if (gestor.TieneResultadosCargados(trabajo.IdTrabajo))
            {
                // CA-03: se advierte que se pierden los tiempos ya cargados
                mensaje = "Hay atletas que ya cargaron tiempos en este trabajo. Si lo eliminás, se pierden esos resultados.\n\n¿Eliminar igual?";
                icono = MessageBoxIcon.Warning;
            }
            else
            {
                mensaje = "¿Seguro que querés eliminar este trabajo? Se borran también sus bloques.";
                icono = MessageBoxIcon.Question;
            }

            if (MessageBox.Show(mensaje, "Confirmar", MessageBoxButtons.YesNo, icono) != DialogResult.Yes)
                return;

            gestor.EliminarTrabajo(trabajo.IdTrabajo);
            MessageBox.Show("Trabajo eliminado.", "Listo");
            CargarTrabajos();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
