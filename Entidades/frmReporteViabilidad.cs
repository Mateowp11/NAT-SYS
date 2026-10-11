// Proyecto: NatSys.UI
//
// RD-06.1 Reporte de viabilidad de marca. La pantalla solo junta los datos
// y muestra el resultado: todo el procesamiento (progresion del RP,
// tendencia, proyeccion y veredicto) lo hace GestorReportes en la BLL.

using System.Collections.Generic;
using System.Linq;
using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmReporteViabilidad : Form
    {
        private List<Prueba> _pruebas = new List<Prueba>();

        // El ultimo reporte generado (para exportarlo). null si no hay.
        private ReporteViabilidad? _reporte;

        public frmReporteViabilidad()
        {
            InitializeComponent();
            CargarCombos();
            dtpFechaObjetivo.Value = DateTime.Today.AddMonths(3); // propuesta: dentro de 3 meses
            LimpiarResultado();
        }

        // Todos los atletas (tambien inactivos: su historial sigue sirviendo)
        // y todas las pruebas
        private void CargarCombos()
        {
            foreach (var atleta in GestorAtleta.GetInstancia().ObtenerTodos())
                cmbAtleta.Items.Add(new OpcionCombo(atleta.IdPersona, atleta.GetNombreCompleto()));

            _pruebas = GestorPruebas.GetInstancia().ObtenerTodas();
            foreach (var prueba in _pruebas)
                cmbPrueba.Items.Add(new OpcionCombo(prueba.IdPrueba, prueba.Nombre));

            cmbLargoPileta.SelectedIndex = 0; // "25"
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            LimpiarResultado();

            var atleta = cmbAtleta.SelectedItem as OpcionCombo;
            var opcionPrueba = cmbPrueba.SelectedItem as OpcionCombo;
            if (atleta == null || opcionPrueba == null)
            {
                MessageBox.Show("Elegí el atleta y la prueba.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Prueba prueba = _pruebas.First(p => p.IdPrueba == opcionPrueba.Id);
            int largoPileta = int.Parse((string)cmbLargoPileta.SelectedItem!);

            try
            {
                _reporte = GestorReportes.GetInstancia().GenerarReporteViabilidad(
                    atleta.Id, prueba, largoPileta, txtMarcaObjetivo.Text.Trim(), dtpFechaObjetivo.Value);
            }
            catch (ArgumentException ex) // formato de tiempo
            {
                MessageBox.Show(ex.Message, "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (InvalidOperationException ex) // sin marcas para analizar
            {
                MessageBox.Show(ex.Message, "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MostrarReporte(_reporte);
        }

        private void MostrarReporte(ReporteViabilidad r)
        {
            lblRecordPersonal.Text = $"Récord personal: {r.RecordPersonal} ({r.FechaRecordPersonal:dd/MM/yyyy}) — {r.CantidadMarcas} marcas analizadas";
            lblTendencia.Text = "Tendencia: " + ExportadorPdf.TextoTendencia(r);
            lblProyeccion.Text = $"Proyección al {r.FechaObjetivo:dd/MM/yyyy}: {r.Proyeccion}";
            lblVeredicto.Text = "Veredicto: " + r.Veredicto.ToUpper();
            lblConclusion.Text = r.Conclusion;

            // Verde, naranja o rojo segun el veredicto
            if (r.Veredicto == "Poco realista")
                lblVeredicto.ForeColor = Color.Firebrick;
            else if (r.Veredicto == "Ambiciosa")
                lblVeredicto.ForeColor = Color.DarkOrange;
            else
                lblVeredicto.ForeColor = Color.ForestGreen;

            dgvProgresion.DataSource = r.Progresion;
            btnExportarPdf.Enabled = true;
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            var reporte = _reporte;
            if (reporte == null) return;

            string atleta = cmbAtleta.SelectedItem?.ToString() ?? "";
            ExportadorPdf.GuardarConDialogo(
                $"Viabilidad {atleta} {reporte.Prueba}",
                ruta => ExportadorPdf.ExportarReporteViabilidad(ruta, atleta, reporte));
        }

        // Si cambia algun dato, el reporte de pantalla ya no corresponde:
        // se borra para no exportar algo distinto de lo que se pidio.
        private void Dato_Cambiado(object sender, EventArgs e)
        {
            LimpiarResultado();
        }

        private void LimpiarResultado()
        {
            _reporte = null;
            lblRecordPersonal.Text = "";
            lblTendencia.Text = "";
            lblProyeccion.Text = "";
            lblVeredicto.Text = "";
            lblConclusion.Text = "";
            dgvProgresion.DataSource = null;
            btnExportarPdf.Enabled = false;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
