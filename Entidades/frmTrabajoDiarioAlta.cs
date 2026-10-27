// Proyecto: NatSys.UI
// Iteracion 2 - CU-07 Agregar trabajo (pasos 6 a 11)

using NatSys.BLL;
using NatSys.Entidades;
using System.IO;

namespace NatSys.UI
{
    public partial class frmTrabajoDiarioAlta : Form
    {
        private readonly int _idGrupo;

        // El archivo elegido (opcional): un Excel o una imagen. Se lee entero
        // al adjuntarlo y viaja al gestor como bytes, que es como se guarda
        // en la base. Las imagenes ya quedan achicadas aca.
        private byte[]? _archivo;
        private string? _archivoNombre;

        public frmTrabajoDiarioAlta(int idGrupo, string nombreGrupo, DateTime fechaPropuesta)
        {
            InitializeComponent();
            _idGrupo = idGrupo;

            lblGrupo.Text = $"Grupo: {nombreGrupo}";
            dtpFecha.Value = fechaPropuesta;

            cmbTipo.Items.Add(TrabajoDiario.TipoPileta);
            cmbTipo.Items.Add(TrabajoDiario.TipoGimnasio);
            cmbTipo.SelectedIndex = 0;
        }

        // Los dias de gimnasio no tienen serie principal (regla del gestor):
        // la grilla de bloques se apaga para que no se carguen por error
        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esPileta = (cmbTipo.SelectedItem as string) == TrabajoDiario.TipoPileta;

            dgvBloques.Enabled = esPileta;
            btnQuitarBloque.Enabled = esPileta;
            lblBloques.Text = esPileta
                ? "Bloques de la serie principal (uno por fila):"
                : "Los trabajos de gimnasio no llevan bloques: alcanza con el archivo.";
        }

        // CU-07 paso 8
        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            using var dialogo = new OpenFileDialog
            {
                Title = "Elegí el Excel o la imagen del trabajo",
                Filter = "Excel o imagen (*.xlsx;*.xls;*.xlsm;*.jpg;*.jpeg;*.png)|*.xlsx;*.xls;*.xlsm;*.jpg;*.jpeg;*.png"
                       + "|Excel (*.xlsx;*.xls;*.xlsm)|*.xlsx;*.xls;*.xlsm"
                       + "|Imágenes (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (dialogo.ShowDialog() != DialogResult.OK) return;

            // Se avisa antes de leer: no tiene sentido cargar en memoria un
            // archivo enorme para despues rechazarlo. El tope es el del gestor.
            if (new FileInfo(dialogo.FileName).Length > GestorTrabajoDiario.TamanioMaximoArchivo)
            {
                MessageBox.Show("El archivo no puede pesar más de 15 MB.",
                    "Archivo muy grande", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                byte[] bytes = File.ReadAllBytes(dialogo.FileName);
                string nombre = Path.GetFileName(dialogo.FileName);

                if (AchicadorImagenes.EsImagen(nombre))
                    bytes = AchicadorImagenes.Achicar(bytes, ref nombre);

                _archivo = bytes;
                _archivoNombre = nombre;
                txtArchivo.Text = $"{nombre} ({FormatearTamanio(bytes.Length)})";
            }
            catch (IOException)
            {
                MessageBox.Show("No se pudo leer el archivo. Si está abierto en otro programa, cerralo y volvé a intentar.",
                    "No se pudo adjuntar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException ex)
            {
                // La imagen no se pudo leer (archivo dañado o con otro formato)
                MessageBox.Show(ex.Message, "No se pudo adjuntar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 245760 -> "240 KB" ; 3145728 -> "3,0 MB"
        private static string FormatearTamanio(int bytes)
        {
            return bytes < 1024 * 1024
                ? $"{bytes / 1024} KB"
                : $"{bytes / (1024.0 * 1024.0):0.0} MB";
        }

        private void btnQuitarArchivo_Click(object sender, EventArgs e)
        {
            _archivo = null;
            _archivoNombre = null;
            txtArchivo.Text = "";
        }

        private void btnQuitarBloque_Click(object sender, EventArgs e)
        {
            var fila = dgvBloques.CurrentRow;
            if (fila == null || fila.IsNewRow) return;

            dgvBloques.Rows.Remove(fila);
        }

        // Pasa las filas de la grilla a objetos Bloque, en el orden en que
        // estan. Aca solo se controla que los numeros sean numeros (eso es
        // de la pantalla); que sean mayores a 0 y que haya descripcion lo
        // valida el gestor.
        private List<Bloque> LeerBloques()
        {
            var bloques = new List<Bloque>();
            if (!dgvBloques.Enabled) return bloques; // gimnasio

            foreach (DataGridViewRow fila in dgvBloques.Rows)
            {
                if (fila.IsNewRow) continue; // la fila vacia de abajo para agregar

                string descripcion = Convert.ToString(fila.Cells[colDescripcion.Index].Value)?.Trim() ?? "";
                string distancia = Convert.ToString(fila.Cells[colDistancia.Index].Value)?.Trim() ?? "";
                string repeticiones = Convert.ToString(fila.Cells[colRepeticiones.Index].Value)?.Trim() ?? "";

                if (descripcion == "" && distancia == "" && repeticiones == "")
                    continue; // fila que quedo en blanco

                int numero = bloques.Count + 1;

                if (!int.TryParse(distancia, out int metros))
                    throw new ArgumentException($"Bloque {numero}: la distancia tiene que ser un número entero de metros.");

                if (!int.TryParse(repeticiones, out int cantidad))
                    throw new ArgumentException($"Bloque {numero}: las repeticiones tienen que ser un número entero.");

                bloques.Add(new Bloque
                {
                    Descripcion = descripcion,
                    Distancia = metros,
                    Repeticiones = cantidad
                });
            }

            return bloques;
        }

        // CU-07 pasos 10 y 11
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            dgvBloques.EndEdit(); // confirma la celda que se estaba escribiendo

            try
            {
                var trabajo = new TrabajoDiario
                {
                    IdGrupoEntrenamiento = _idGrupo,
                    Fecha = dtpFecha.Value.Date,
                    Tipo = cmbTipo.SelectedItem as string ?? "",
                    ArchivoNombre = _archivoNombre,
                    Archivo = _archivo,
                    Bloques = LeerBloques()
                };

                GestorTrabajoDiario.GetInstancia().AgregarTrabajo(trabajo);
                MessageBox.Show("Trabajo guardado.", "Listo");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException ex)
            {
                // CA-01 (sin archivo ni bloques) y los demas datos mal cargados:
                // el formulario queda abierto para corregir
                MessageBox.Show(ex.Message, "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // CA-04
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
