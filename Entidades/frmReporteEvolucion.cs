// Proyecto: NatSys.UI
//
// RF-06.2 Analisis de evolucion por estilo: el reporte CON GRAFICO.
// GestorReportes (BLL) calcula el % de mejora de cada marca y el resumen
// por prueba; esta pantalla lo dibuja con ScottPlot (paquete NuGet
// ScottPlot.WinForms) y lo puede exportar a PDF.

using System.Linq;
using NatSys.BLL;

namespace NatSys.UI
{
    public partial class frmReporteEvolucion : Form
    {
        // El grafico de ScottPlot. Se crea por codigo y se mete dentro de
        // pnlGrafico (asi respeta el tamaño del panel aunque Windows tenga
        // la pantalla escalada al 125 % o 150 %).
        private readonly ScottPlot.WinForms.FormsPlot _grafico;

        // El ultimo reporte generado (para exportarlo). null si no hay.
        private ReporteEvolucion? _reporte;

        public frmReporteEvolucion()
        {
            InitializeComponent();

            _grafico = new ScottPlot.WinForms.FormsPlot { Dock = DockStyle.Fill };
            pnlGrafico.Controls.Add(_grafico);

            // El eje de abajo muestra fechas en vez de numeros
            _grafico.Plot.Axes.DateTimeTicksBottom();

            CargarFiltros();
            LimpiarResultado();
        }

        private void CargarFiltros()
        {
            foreach (var atleta in GestorAtleta.GetInstancia().ObtenerTodos())
                cmbAtleta.Items.Add(new OpcionCombo(atleta.IdPersona, atleta.GetNombreCompleto()));

            cmbLargoPileta.SelectedIndex = 0; // "25"
            cmbEstilo.SelectedIndex = 0;      // "Todos" (los estilos estan en el Designer)

            // Por defecto: la ultima temporada (los ultimos 12 meses)
            dtpHasta.Value = DateTime.Today;
            dtpDesde.Value = DateTime.Today.AddYears(-1);
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            LimpiarResultado();

            var atleta = cmbAtleta.SelectedItem as OpcionCombo;
            if (atleta == null)
            {
                MessageBox.Show("Elegí un atleta.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int largoPileta = int.Parse((string)cmbLargoPileta.SelectedItem!);

            // La primera opcion ("Todos") significa no filtrar por estilo
            string? estilo = cmbEstilo.SelectedIndex > 0 ? (string)cmbEstilo.SelectedItem! : null;

            try
            {
                _reporte = GestorReportes.GetInstancia().GenerarReporteEvolucion(
                    atleta.Id, largoPileta, estilo, dtpDesde.Value, dtpHasta.Value);
            }
            catch (ArgumentException ex) // fechas al reves
            {
                MessageBox.Show(ex.Message, "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (InvalidOperationException ex) // sin marcas en el periodo
            {
                MessageBox.Show(ex.Message, "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DibujarGrafico(_reporte);
            dgvResumen.DataSource = _reporte.Resumen;
            btnExportarPdf.Enabled = true;

            if (_reporte.Series.Count == 0)
                MessageBox.Show("Ninguna prueba tiene 2 marcas o más en el período, así que no hay líneas para graficar. " +
                    "El resumen se muestra igual.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Una linea por prueba: eje X = fecha, eje Y = % de mejora respecto
        // de la primera marca del periodo (por eso todas arrancan en 0 y se
        // pueden comparar aunque sean pruebas de distancias distintas)
        private void DibujarGrafico(ReporteEvolucion reporte)
        {
            var plot = _grafico.Plot;
            plot.Clear();

            foreach (var serie in reporte.Series)
            {
                var linea = plot.Add.Scatter(serie.Fechas, serie.PorcentajesMejora);
                linea.LegendText = serie.Prueba;
                linea.LineWidth = 2;
                linea.MarkerSize = 7;
            }

            // Linea de referencia en 0 % = la primera marca del periodo
            plot.Add.HorizontalLine(0);

            plot.Title($"Evolución - {reporte.Estilo} - pileta {reporte.LargoPileta} m");
            plot.XLabel("Fecha");
            plot.YLabel("% de mejora vs. primera marca");
            plot.ShowLegend();
            plot.Axes.AutoScale();
            _grafico.Refresh();
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            var reporte = _reporte;
            if (reporte == null) return;

            string atleta = cmbAtleta.SelectedItem?.ToString() ?? "";

            ExportadorPdf.GuardarConDialogo($"Evolucion {atleta}", ruta =>
            {
                // El grafico se guarda como imagen temporal y se pega en el PDF
                string imagen = Path.Combine(Path.GetTempPath(), "natsys_grafico_evolucion.png");
                _grafico.Plot.SavePng(imagen, 1000, 450);

                ExportadorPdf.ExportarReporteEvolucion(ruta, atleta, reporte, imagen);
            });
        }

        // Si cambia algun filtro, el reporte de pantalla ya no corresponde
        private void Filtro_Cambiado(object sender, EventArgs e)
        {
            LimpiarResultado();
        }

        private void LimpiarResultado()
        {
            _reporte = null;
            dgvResumen.DataSource = null;
            btnExportarPdf.Enabled = false;

            // _grafico todavia no existe cuando InitializeComponent dispara
            // los primeros eventos de los filtros
            if (_grafico != null)
            {
                _grafico.Plot.Clear();
                _grafico.Refresh();
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
