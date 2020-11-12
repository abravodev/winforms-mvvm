using MvvmTools.Core;
using System.ComponentModel;
using System.Windows.Forms;

namespace MvvmTools.Bindings
{
    public static class DataGridViewBindingExtensions
    {
        public static void AddBinding<TSource>(this DataGridView dataGridView, BindingList<TSource> items)
        {
            dataGridView.DataSource = new BindingSource(items, null);
        }

        public static void WithContextMenu<T>(this DataGridView dataGridView, params (string Name, ICommand<T> Command)[] menuItems)
        {
            dataGridView.ContextMenuStrip = new ContextMenuStrip();
            foreach (var menuItem in menuItems)
            {
                dataGridView.ContextMenuStrip.Items.Add(menuItem.Name, null, new System.EventHandler((sender, e) =>
                {
                    var selectedUser = (T) dataGridView.ContextMenuStrip.Tag;
                    menuItem.Command.Execute(selectedUser);
                }));
            }

            dataGridView.CellMouseDown += new DataGridViewCellMouseEventHandler((sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    var selectedItem = dataGridView.Rows[e.RowIndex].DataBoundItem;
                    dataGridView.ContextMenuStrip.Tag = selectedItem;
                    dataGridView.ContextMenuStrip.Show(dataGridView, e.Location);
                }
            });
        }
    }
}
