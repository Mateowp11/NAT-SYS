// Proyecto: NatSys.UI
//
// Este formulario sirve para ALTA y MODIFICAR: si se abre con el
// constructor vacio, es un atleta nuevo; si se abre pasandole un Atleta
// existente, precarga los datos y al guardar llama a ModificarAtleta en
// vez de AgregarAtleta.

using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmAtletaAlta : Form
    {
        private readonly Atleta _atletaAEditar; // null = alta, no null = modificar

        public frmAtletaAlta()
        {
            InitializeComponent();
            _atletaAEditar = null;
            this.Text = "Nuevo atleta";
        }

        public frmAtletaAlta(Atleta atleta)
        {
            InitializeComponent();
            _atletaAEditar = atleta;
            this.Text = "Modificar atleta";

            txtNombre.Text = atleta.Nombre;
            txtApellido.Text = atleta.Apellido;
            dtpFechaNacimiento.Value = atleta.FechaNacimiento;
            txtCategoria.Text = atleta.Categoria;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_atletaAEditar == null)
                {
                    GestorAtleta.GetInstancia().AgregarAtleta(
                        txtNombre.Text,
                        txtApellido.Text,
                        dtpFechaNacimiento.Value,
                        txtCategoria.Text,
                        null);
                }
                else
                {
                    GestorAtleta.GetInstancia().ModificarAtleta(
                        _atletaAEditar.IdPersona,
                        txtNombre.Text,
                        txtApellido.Text,
                        dtpFechaNacimiento.Value,
                        txtCategoria.Text);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

