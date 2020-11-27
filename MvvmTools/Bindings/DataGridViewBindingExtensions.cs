using MvvmTools.Controls.DataGridViewControl;
using MvvmTools.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MvvmTools.Bindings
{
    public static class DataGridViewBindingExtensions
    {
        public static void AddBinding<TSource>(this DataGridView dataGridView, BindingList<TSource> items)
        {
            dataGridView.DataSource = new BindingSource(items, null);
        }

        public static void WithContextMenu<T>(this DataGridView dataGridView, params (string Name, ICommand<T> Command, Image Image)[] menuItems)
        {
            dataGridView.ContextMenuStrip = new ContextMenuStrip();
            dataGridView.ContextMenuStrip.AccessibleName = $"Context menu of {dataGridView.AccessibleName}";
            foreach (var menuItem in menuItems)
            {
                dataGridView.ContextMenuStrip.Items.Add(menuItem.Name, menuItem.Image, new EventHandler((sender, e) =>
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

        public static Bind<TBinding> For<TBinding, TDataGridView, TSource>(this Bind<TBinding> item, TDataGridView datagridView, Func<TBinding, BindingList<TSource>> items)
            where TDataGridView : DataGridView
        {
            datagridView.AddBinding(items(item._item));
            return item;
        }

        public static Bind<TBinding> For<TBinding, TSource>(this Bind<TBinding> item, BindedAdvancedDataGridView datagridView, Func<TBinding, BindingList<TSource>> items)
        {
            var list = items(item._item);
            datagridView.Bind(list);
            datagridView.AddBinding(list);
            return item;
        }

        public static Bind<TBinding> WithContextMenu<TBinding, TSource>(this Bind<TBinding> item, DataGridView dataGridView, params MenuOption<TSource>[] menuItems)
        {
            dataGridView.WithContextMenu(menuItems.Select(x => (x.Name, x.Command, x.Image)).ToArray());
            return item;
        }
    }
}
