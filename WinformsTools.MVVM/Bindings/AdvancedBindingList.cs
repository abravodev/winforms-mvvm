using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic;
using WinformsTools.MVVM.Controls.DataGridViewControl;

namespace WinformsTools.MVVM.Bindings
{
    /// <summary>
    /// Binding list that supports multiple sorting
    /// <see href="https://www.codeproject.com/Articles/19867/How-To-Allow-To-Sort-By-Multiple-Columns-in-Custom"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AdvancedBindingList<T> : BindingList<T>, IBindingListView
    {
        private ListSortDescriptionCollection _sortDescriptions = new ListSortDescriptionCollection();

        private bool _isSorted;
        private BindingList<T> _source;
        private IList<T> SourceItems => _source ?? Items;

        public AdvancedBindingList() { }

        public AdvancedBindingList(IList<T> list) : base(list) { }

        public AdvancedBindingList(BindingList<T> source)
        {
            _source = source;
            _source.ListChanged += (sender, e) => SyncSource(e);
            if (_source.Any())
            {
                this.Recalculate();
            }
        }

        private void SyncSource(ListChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Filter) || _sortDescriptions.Count > 0)
            {
                this.Recalculate();
                return;
            }

            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                this.Insert(e.NewIndex, _source[e.NewIndex]);
                return;
            }

            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                this.RemoveAt(e.NewIndex);
                return;
            }

            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                this[e.NewIndex] = _source[e.NewIndex];
                return;
            }

            if (e.ListChangedType == ListChangedType.ItemMoved)
            {
                var item = this[e.OldIndex];
                this.Remove(item);
                this.Insert(e.NewIndex, item);
                return;
            }

            // For rest of cases, we replace the whole thing
            this.Replace(_source);
        }

        public ListSortDescriptionCollection SortDescriptions => _sortDescriptions;

        public bool SupportsAdvancedSorting => true;

        public bool SupportsFiltering => true;

        protected override bool IsSortedCore => _isSorted;

        private string _filter;
        public string Filter { get => _filter; set => ApplyFilter(value); }

        private void ApplyFilter(string stringFilter)
        {
            _filter = stringFilter;
            this.Replace(GetFiltered());
        }

        private void Replace(IList<T> items)
        {
            this.Clear();
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        private void Recalculate()
        {
            ApplyFilter(Filter);
            ApplySort(_sortDescriptions);
        }

        private IList<T> SortedSourceItems()
        {
            var items = SourceItems.ToList();
            SortList(items);
            return items;
        }

        public IList<T> GetFiltered()
        {
            if (string.IsNullOrEmpty(Filter))
            {
                return SortedSourceItems();
            }

            return SortedSourceItems().Where(FilterConverter.Convert(Filter)).ToList();
        }

        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            List<T> items = this.Items as List<T>;

            if (items != null)
            {
                _sortDescriptions = sorts;
                SortList(items);
                //_isSorted = false;
            }
            else
            {
                //_isSorted = false;
            }

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        private void SortList(List<T> items)
        {
            Comparison<T> comparer = ListObjectComparer.FromSort<T>(_sortDescriptions).CompareValuesByProperties;
            items.Sort(comparer);
        }

        public void RemoveFilter() => this.Filter = "";

        protected override void RemoveSortCore() => _isSorted = false;
    }
}
