// Proyecto: NatSys.UI

using System.Collections.Generic;
using System.Linq;
using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmAsignarPermisos : Form
    {
        private readonly int _idGrupo;
        private List<Permiso> _permisosDelGrupo = new List<Permiso>();

        public frmAsignarPermisos(int idGrupo, string nombreGrupo)
        {
            InitializeComponent();
            _idGrupo = idGrupo;
            this.Text = $"Permisos de: {nombreGrupo}";
            CargarPermisos();
        }

        private void CargarPermisos()
        {
            var todosLosPermisos = GestorGrupos.GetInstancia().ObtenerPermisosDisponibles();
            var grupo = GestorGrupos.GetInstancia().ObtenerTodos().First(g => g.IdGrupo == _idGrupo);
            _permisosDelGrupo = grupo.Permisos;

            clbPermisos.DisplayMember = nameof(Permiso.Nombre);
            clbPermisos.Items.Clear();

            foreach (var permiso in todosLosPermisos)
            {
                bool yaAsignado = _permisosDelGrupo.Any(p => p.IdPermiso == permiso.IdPermiso);
                clbPermisos.Items.Add(permiso, yaAsignado);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbPermisos.Items.Count; i++)
            {
                var permiso = (Permiso)clbPermisos.Items[i];
                bool marcado = clbPermisos.GetItemChecked(i);
                bool yaAsignado = _permisosDelGrupo.Any(p => p.IdPermiso == permiso.IdPermiso);

                if (marcado && !yaAsignado)
                {
                    GestorGrupos.GetInstancia().AsignarPermiso(_idGrupo, permiso.IdPermiso);
                }
                else if (!marcado && yaAsignado)
                {
                    GestorGrupos.GetInstancia().QuitarPermiso(_idGrupo, permiso.IdPermiso);
                }
            }

            MessageBox.Show("Permisos actualizados.", "Listo");
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}