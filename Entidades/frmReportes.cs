// Proyecto: NatSys.UI
//
// Menu de reportes (RF-06): desde aca se abre cada uno de los dos.

namespace NatSys.UI
{
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
        }

        // RD-06.1
        private void btnViabilidad_Click(object sender, EventArgs e)
        {
            var frm = new frmReporteViabilidad();
            frm.ShowDialog();
        }

        // RF-06.2 (el que tiene grafico)
        private void btnEvolucion_Click(object sender, EventArgs e)
        {
            var frm = new frmReporteEvolucion();
            frm.ShowDialog();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

