using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace MvvmTools.Controls
{
    [Designer(typeof(BindableToolStripMenuItem), typeof(ToolStripMenuItem))]
    public class BindableToolStripMenuItem : ToolStripMenuItem
    {
        public void AddBinding<TSource>(BindingList<TSource> source, Func<TSource, ToolStripMenuItem> mapToMenuItem)
        {
            source.ListChanged += (object sender, ListChangedEventArgs e) => BindingChanged(e, source, MapToItem(mapToMenuItem));
            var menuItems = source.Select(MapToItem(mapToMenuItem)).ToArray();
            this.DropDownItems.AddRange(menuItems);
        }

        public void AddBinding<TSource>(BindingList<TSource> source, Func<TSource, ToolStripMenuItem> mapToMenuItem, Action<TSource> onClicked)
        {
            AddBinding(source, mapToMenuItem);
            this.DropDownItemClicked += (sender, e) => onClicked(GetClickedItem<TSource>(e));
        }

        public TSource GetClickedItem<TSource>(ToolStripItemClickedEventArgs e)
        {
            var selectedItem = e.ClickedItem as ToolStripMenuItem;
            return (TSource) selectedItem.Tag;
        }

        private Func<TSource, ToolStripMenuItem> MapToItem<TSource>(Func<TSource, ToolStripMenuItem> mapToMenuItem)
        {
            return sourceItem =>
            {
                var menuItem = mapToMenuItem(sourceItem);
                menuItem.Tag = sourceItem;
                return menuItem;
            };
        }

        private void BindingChanged<TSource>(ListChangedEventArgs e, BindingList<TSource> source, Func<TSource, ToolStripMenuItem> mapToMenuItem)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    this.DropDownItems.Add(mapToMenuItem(source[e.NewIndex]));
                    break;
                case ListChangedType.ItemChanged: // TODO: Will this break something ? (instead of update the proper values)
                    var item = mapToMenuItem(source[e.NewIndex]);
                    this.DropDownItems.RemoveAt(e.OldIndex);
                    this.DropDownItems.Insert(e.NewIndex, item);
                    break;
                case ListChangedType.ItemDeleted:
                    this.DropDownItems.RemoveAt(e.OldIndex);
                    break;
                case ListChangedType.Reset: // TODO: Will this break something ?
                    this.DropDownItems.Clear();
                    this.DropDownItems.AddRange(source
                        .Select(mapToMenuItem)
                        .ToArray());
                    break;
                default: throw new NotImplementedException();
            }
        }
    }
}
