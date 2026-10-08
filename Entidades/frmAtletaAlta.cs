// Proyecto: NatSys.UI
//
// Este formulario sirve para ALTA y MODIFICAR: si se abre con el
// constructor vacio, es un atleta nuevo; si se abre pasandole un Atleta
// existente, precarga los datos y al guardar llama a ModificarAtleta en
// vez de AgregarAtleta.
//
// Tambien permite tildar las pruebas principales del atleta: es una
// precondicion de CU-01 (no se pueden calcular pasajes de una prueba que
// el atleta no tiene asignada en su perfil).

using System.Collections.Generic;
using System.Linq;
using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmAtletaAlta : Form
    {
        private readonly Atleta? _atletaAEditar; // null = alta, no null = modificar

        public frmAtletaAlta()
        {
            InitializeComponent();
            _atletaAEditar = null;
            this.Text = "Nuevo atleta";
            CargarPruebas();
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

            CargarPruebas();
        }

        // Llena la lista con TODAS las pruebas, y tilda las que el atleta
        // ya tiene asignadas (en el alta no tiene ninguna, quedan todas
        // destildadas).
        private void CargarPruebas()
        {
            var todasLasPruebas = GestorPruebas.GetInstancia().ObtenerTodas();

            clbPruebas.DisplayMember = nameof(Prueba.Nombre);
            clbPruebas.Items.Clear();

            foreach (var prueba in todasLasPruebas)
            {
                bool yaAsignada = _atletaAEditar != null
                    && _atletaAEditar.Pruebas.Any(p => p.IdPrueba == prueba.IdPrueba);

                clbPruebas.Items.Add(prueba, yaAsignada);
            }
        }

        // Devuelve los Id de las pruebas que quedaron tildadas
        private List<int> ObtenerIdsPruebasTildadas()
        {
            return clbPruebas.CheckedItems
                .Cast<Prueba>()
                .Select(p => p.IdPrueba)
                .ToList();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_atletaAEditar == null)
                {
                    // Alta: las pruebas tildadas se guardan junto con el atleta
                    GestorAtleta.GetInstancia().AgregarAtleta(
                        txtNombre.Text,
                        txtApellido.Text,
                        dtpFechaNacimiento.Value,
                        txtCategoria.Text,
                        ObtenerIdsPruebasTildadas());
                }
                else
                {
                    GestorAtleta.GetInstancia().ModificarAtleta(
                        _atletaAEditar.IdPersona,
                        txtNombre.Text,
                        txtApellido.Text,
                        dtpFechaNacimiento.Value,
                        txtCategoria.Text);

                    ActualizarPruebasAsignadas();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Mismo criterio que frmAsignarPermisos: compara como estaba cada
        // prueba antes contra como quedo, y solo toca lo que cambio.
        private void ActualizarPruebasAsignadas()
        {
            for (int i = 0; i < clbPruebas.Items.Count; i++)
            {
                var prueba = (Prueba)clbPruebas.Items[i];
                bool tildada = clbPruebas.GetItemChecked(i);
                bool yaAsignada = _atletaAEditar!.Pruebas.Any(p => p.IdPrueba == prueba.IdPrueba);

                if (tildada && !yaAsignada)
                    GestorAtleta.GetInstancia().AsignarPrueba(_atletaAEditar.IdPersona, prueba.IdPrueba);
                else if (!tildada && yaAsignada)
                    GestorAtleta.GetInstancia().QuitarPrueba(_atletaAEditar.IdPersona, prueba.IdPrueba);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}