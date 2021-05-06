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
        private ListSortDescriptionCollection _sortDescriptions;

        private bool _isSorted;

        public AdvancedBindingList() { }

        public AdvancedBindingList(IList<T> list) : base(list) { }

        public ListSortDescriptionCollection SortDescriptions => _sortDescriptions;

        public bool SupportsAdvancedSorting => true;

        public bool SupportsFiltering => true;

        protected override bool IsSortedCore => _isSorted;

        private string _filter;
        public string Filter { get => _filter; set => ApplyFilter(value); }

        public event EventHandler FilterChanged;

        private void ApplyFilter(string stringFilter)
        {
            _filter = stringFilter;
            FilterChanged.Invoke(this, new EventArgs());
        }

        public IList<T> GetFiltered()
        {
            if (string.IsNullOrEmpty(Filter))
            {
                return Items;
            }

            return Items.Where(FilterConverter.Convert(Filter)).ToList();
        }

        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            List<T> items = this.Items as List<T>;

            if (items != null)
            {
                _sortDescriptions = sorts;
                Comparison<T> comparer = ListObjectComparer.FromSort<T>(sorts).CompareValuesByProperties;
                items.Sort(comparer);
                //_isSorted = false;
            }
            else
            {
                //_isSorted = false;
            }

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }      

        public void RemoveFilter() => this.Filter = "";

        protected override void RemoveSortCore() => _isSorted = false;
    }
}
