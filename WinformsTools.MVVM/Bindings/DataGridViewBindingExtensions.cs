using WinformsTools.MVVM.Controls.DataGridViewControl;
using WinformsTools.MVVM.Core;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using WinformsTools.Common.Extensions;
using System.ComponentModel;

namespace WinformsTools.MVVM.Bindings
{
    public static class DataGridViewBindingExtensions
    {
        public static void AddBinding<TSource>(this DataGridView dataGridView, BindingList<TSource> items)
        {
            dataGridView.DataSource = new BindingSource(items, null);
            ConfigureColumns<TSource>(dataGridView);
        }

        public static void AddAdvancedBinding<TSource>(this DataGridView dataGridView, BindingList<TSource> items)
        {
            var datagridList = new AdvancedBindingList<TSource>(items);
            dataGridView.DataSource = new BindingSource(datagridList, null);
            ConfigureColumns<TSource>(dataGridView);
        }

        private static void ConfigureColumns<TSource>(DataGridView dataGridView)
        {
            var propertiesWithConfiguration = typeof(TSource)
                .GetProperties()
                .Where(x => x.HasCustomAttribute<TableColumnAttribute>());

            foreach (var property in propertiesWithConfiguration)
            {
                var configuration = property.GetCustomAttribute<TableColumnAttribute>();
                dataGridView.Columns[property.Name].AutoSizeMode = configuration.AutoSizeMode;
            }
        }

        public static void WithContextMenu<T>(this DataGridView dataGridView, params (string Name, ICommand<T> Command, Image Image)[] menuItems)
        {
            dataGridView.ContextMenuStrip = new ContextMenuStrip();
            dataGridView.ContextMenuStrip.AccessibleName = $"Context menu of {dataGridView.AccessibleName}";

            dataGridView.CellMouseDown += new DataGridViewCellMouseEventHandler((sender, e) =>
            {
                if (e.Button != MouseButtons.Right)
                {
                    return;
                }

                if (e.RowIndex == -1)
                {
                    // Click on header
                    dataGridView.ContextMenuStrip.Items.Clear(); // No context menu
                    return;
                }

                SetupContextMenuStrip(dataGridView.ContextMenuStrip, menuItems);
                var selectedItem = dataGridView.Rows[e.RowIndex].DataBoundItem;
                dataGridView.ContextMenuStrip.Tag = selectedItem;
                dataGridView.ContextMenuStrip.Show(dataGridView, e.Location);
            });
        }

        private static void SetupContextMenuStrip<T>(ContextMenuStrip contextMenuStrip, (string Name, ICommand<T> Command, Image Image)[] menuItems)
        {
            contextMenuStrip.Items.Clear();
            foreach (var menuItem in menuItems)
            {
                contextMenuStrip.Items.Add(menuItem.Name, menuItem.Image, new EventHandler((sender, e) =>
                {
                    var tag = (T)contextMenuStrip.Tag;
                    menuItem.Command.Execute(tag);
                }));
            }
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
            datagridView.AddAdvancedBinding(list);
            return item;
        }

        public static Bind<TBinding> WithContextMenu<TBinding, TSource>(this Bind<TBinding> item, DataGridView dataGridView, params MenuOption<TSource>[] menuItems)
        {
            dataGridView.WithContextMenu(menuItems.Select(x => (x.Name, x.Command, x.Image)).ToArray());
            return item;
        }
    }
}
