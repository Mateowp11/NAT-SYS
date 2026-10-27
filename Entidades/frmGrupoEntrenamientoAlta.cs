// Proyecto: NatSys.UI
// Iteracion 2 - CU-06 (prototipo "Agregar grupo")
//
// Sirve para ALTA y MODIFICAR, igual que frmTorneoAlta: constructor vacio
// = grupo nuevo; constructor con un GrupoEntrenamiento = edicion.

using NatSys.BLL;
using NatSys.Entidades;
using System.Linq;

namespace NatSys.UI
{
    public partial class frmGrupoEntrenamientoAlta : Form
    {
        private readonly GrupoEntrenamiento? _grupoAEditar;

        public frmGrupoEntrenamientoAlta()
        {
            InitializeComponent();
            _grupoAEditar = null;
            this.Text = "Agregar grupo";
            btnGuardar.Text = "Crear grupo";

            CargarEntrenadores(null);
            CargarAtletas(new List<Atleta>());
        }

        // CU-06 paso 10: el formulario con los datos actuales y los atletas ya tildados
        public frmGrupoEntrenamientoAlta(GrupoEntrenamiento grupo)
        {
            InitializeComponent();
            _grupoAEditar = grupo;
            this.Text = "Modificar grupo";
            btnGuardar.Text = "Guardar cambios";

            txtNombre.Text = grupo.Nombre;
            CargarEntrenadores(grupo.Entrenador);
            CargarAtletas(grupo.Atletas);
        }

        // Combo "Seleccionar entrenador...". Si el grupo tiene un entrenador
        // que hoy no esta activo, se lo agrega igual para no perderlo.
        private void CargarEntrenadores(Entrenador? actual)
        {
            var entrenadores = GestorGrupoEntrenamiento.GetInstancia().ObtenerEntrenadoresActivos();

            if (actual != null && !entrenadores.Any(x => x.IdPersona == actual.IdPersona))
                entrenadores.Add(actual);

            cmbEntrenador.Items.Clear();
            foreach (var entrenador in entrenadores)
                cmbEntrenador.Items.Add(new OpcionCombo(entrenador.IdPersona, entrenador.GetNombreCompleto()));

            OpcionCombo.Seleccionar(cmbEntrenador, actual?.IdPersona);
        }

        // CU-06 paso 5: los atletas activos para tildar. Un atleta del grupo
        // que hoy esta inactivo tambien aparece (tildado), para que modificar
        // el grupo no lo saque sin que nadie lo haya pedido.
        private void CargarAtletas(List<Atleta> delGrupo)
        {
            var atletas = GestorAtleta.GetInstancia().GetAtletasActivos();

            foreach (var atleta in delGrupo)
            {
                if (!atletas.Any(x => x.IdPersona == atleta.IdPersona))
                    atletas.Add(atleta);
            }

            clbAtletas.Items.Clear();
            foreach (var atleta in atletas.OrderBy(x => x.Apellido).ThenBy(x => x.Nombre))
            {
                string texto = atleta.GetNombreCompleto() + (atleta.EstaActivo() ? "" : " (inactivo)");
                bool tildado = delGrupo.Any(x => x.IdPersona == atleta.IdPersona);
                clbAtletas.Items.Add(new OpcionCombo(atleta.IdPersona, texto), tildado);
            }

            ActualizarCantidad(clbAtletas.CheckedItems.Count);
        }

        // ItemCheck avisa ANTES de que cambie la tilde, por eso se suma o se
        // resta uno a mano
        private void clbAtletas_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int cantidad = clbAtletas.CheckedItems.Count + (e.NewValue == CheckState.Checked ? 1 : -1);
            ActualizarCantidad(cantidad);
        }

        private void ActualizarCantidad(int cantidad)
        {
            lblCantidad.Text = cantidad == 1 ? "1 atleta seleccionado" : $"{cantidad} atletas seleccionados";
        }

        // CU-06 pasos 7-8 (Nuevo) y 12 (Modificar)
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idEntrenador = (cmbEntrenador.SelectedItem as OpcionCombo)?.Id ?? 0;
            var idsAtletas = clbAtletas.CheckedItems
                .Cast<OpcionCombo>()
                .Select(o => o.Id)
                .ToList();

            try
            {
                var gestor = GestorGrupoEntrenamiento.GetInstancia();

                if (_grupoAEditar == null)
                {
                    gestor.AgregarGrupo(txtNombre.Text, idEntrenador, idsAtletas);
                    MessageBox.Show("Grupo creado.", "Listo");
                }
                else
                {
                    gestor.ModificarGrupo(_grupoAEditar.IdGrupoEntrenamiento, txtNombre.Text, idEntrenador, idsAtletas);
                    MessageBox.Show("Grupo modificado.", "Listo");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException ex)
            {
                // CA-01: se resaltan los campos que faltan y el formulario
                // queda abierto para corregir (se retoma desde el paso 6 u 11)
                ResaltarFaltantes();
                MessageBox.Show(ex.Message, "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Solo pinta: la regla de que son obligatorios esta en el gestor
        private void ResaltarFaltantes()
        {
            txtNombre.BackColor = string.IsNullOrWhiteSpace(txtNombre.Text) ? Color.MistyRose : SystemColors.Window;
            cmbEntrenador.BackColor = cmbEntrenador.SelectedItem == null ? Color.MistyRose : SystemColors.Window;
            clbAtletas.BackColor = clbAtletas.CheckedItems.Count == 0 ? Color.MistyRose : SystemColors.Window;
        }

        // CA-03: se descartan los cambios
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
