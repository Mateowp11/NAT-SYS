// Proyecto: NatSys.UI

using System.Collections.Generic;
using System.Linq;
using NatSys.BLL;
using NatSys.Entidades;

namespace NatSys.UI
{
    public partial class frmAsignarGrupoUsuario : Form
    {
        private readonly int _idUsuario;
        private List<Grupo> _gruposDelUsuario = new List<Grupo>();

        public frmAsignarGrupoUsuario(int idUsuario, string nombreUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            this.Text = $"Grupos de: {nombreUsuario}";
            CargarGrupos();
        }

        private void CargarGrupos()
        {
            var todosLosGrupos = GestorGrupos.GetInstancia().ObtenerTodos();
            var usuario = GestorUsuarios.GetInstancia().ObtenerTodos().First(u => u.IdUsuario == _idUsuario);
            _gruposDelUsuario = usuario.Grupos;

            clbGrupos.DisplayMember = nameof(Grupo.Nombre);
            clbGrupos.Items.Clear();

            foreach (var grupo in todosLosGrupos)
            {
                bool yaAsignado = _gruposDelUsuario.Any(g => g.IdGrupo == grupo.IdGrupo);
                clbGrupos.Items.Add(grupo, yaAsignado);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbGrupos.Items.Count; i++)
            {
                var grupo = (Grupo)clbGrupos.Items[i];
                bool marcado = clbGrupos.GetItemChecked(i);
                bool yaAsignado = _gruposDelUsuario.Any(g => g.IdGrupo == grupo.IdGrupo);

                if (marcado && !yaAsignado)
                {
                    GestorUsuarios.GetInstancia().AsignarGrupo(_idUsuario, grupo.IdGrupo);
                }
                else if (!marcado && yaAsignado)
                {
                    GestorUsuarios.GetInstancia().QuitarGrupo(_idUsuario, grupo.IdGrupo);
                }
            }

            MessageBox.Show("Grupos actualizados.", "Listo");
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}