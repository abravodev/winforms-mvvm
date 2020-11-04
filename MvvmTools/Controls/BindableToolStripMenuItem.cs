using MvvmTools.Components;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace MvvmTools.Controls
{
    [Designer(typeof(BindableToolStripMenuItem), typeof(ToolStripMenuItem))]
    public class BindableToolStripMenuItem : ToolStripMenuItem
    {
        public void AddBinding<TSource>(BindingList<TSource> source)
            where TSource : IMenuOption
        {
            source.ListChanged += (object sender, ListChangedEventArgs e) 
                => ApplicationDispatcher.Invoke(() => BindingChanged(e, source));
            var menuItems = source.Select(MapToItem).ToArray();
            this.DropDownItems.AddRange(menuItems);
        }

        public void AddBinding<TSource>(BindingList<TSource> source, Action<TSource> onClicked)
            where TSource: IMenuOption
        {
            AddBinding(source);
            AddClickAction(onClicked);
        }

        public void AddClickAction<TSource>(Action<TSource> onClicked)
        {
            this.DropDownItemClicked += (sender, e) => onClicked(GetClickedItem<TSource>(e));
        }

        public TSource GetClickedItem<TSource>(ToolStripItemClickedEventArgs e)
        {
            var selectedItem = e.ClickedItem as ToolStripMenuItem;
            return (TSource) selectedItem.Tag;
        }

        private ToolStripMenuItem MapToItem<TSource>(TSource sourceItem)
            where TSource : IMenuOption
        {
            return new ToolStripMenuItem
            {
                Text = sourceItem.Text,
                Checked = sourceItem.Checked,
                Tag = sourceItem
            };
        }

        private void BindingChanged<TSource>(ListChangedEventArgs e, BindingList<TSource> source)
            where TSource : IMenuOption
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    this.DropDownItems.Add(MapToItem(source[e.NewIndex]));
                    break;
                case ListChangedType.ItemChanged: // TODO: Will this break something ? (instead of update the proper values)
                    var item = MapToItem(source[e.NewIndex]);
                    this.DropDownItems.RemoveAt(e.OldIndex);
                    this.DropDownItems.Insert(e.NewIndex, item);
                    break;
                case ListChangedType.ItemDeleted:
                    this.DropDownItems.RemoveAt(e.OldIndex);
                    break;
                case ListChangedType.Reset: // TODO: Will this break something ?
                    this.DropDownItems.Clear();
                    this.DropDownItems.AddRange(source
                        .Select(MapToItem)
                        .ToArray());
                    break;
                default: throw new NotImplementedException();
            }
        }
    }
}
