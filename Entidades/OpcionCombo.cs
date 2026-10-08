// Proyecto: NatSys.UI
//
// Un elemento generico para cargar en un ComboBox: guarda un Id (el que
// necesitamos para trabajar) y el texto que ve el usuario.
//
// Por que hace falta: un ComboBox muestra el resultado de ToString() de
// cada elemento. Si cargaramos un Atleta directo, mostraria
// "NatSys.Entidades.Atleta". Con esta clase controlamos que texto se ve
// ("Juan Perez") sin perder el Id que hay que mandarle al Gestor.

namespace NatSys.UI
{
    public class OpcionCombo
    {
        public int Id { get; }
        public string Texto { get; }

        public OpcionCombo(int id, string texto)
        {
            Id = id;
            Texto = texto;
        }

        // Lo que el ComboBox muestra en pantalla
        public override string ToString() => Texto;

        // Deja seleccionada en el combo la opcion que tenga ese Id.
        // Si el Id es null o no esta en la lista, el combo queda vacio.
        public static void Seleccionar(ComboBox combo, int? id)
        {
            combo.SelectedIndex = -1;

            for (int i = 0; i < combo.Items.Count; i++)
            {
                if (combo.Items[i] is OpcionCombo opcion && opcion.Id == id)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }
        }
    }
}