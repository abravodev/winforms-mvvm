using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WinformsTools.MVVM.Bindings
{
    public static class ListObjectComparer
    {
        public static ListObjectComparer<T> FromSort<T>(ListSortDescriptionCollection sorts)
        {
            var comparers = sorts.Cast<ListSortDescription>()
                .Select(MakeComparer<T>)
                .ToList();
            return new ListObjectComparer<T>(comparers);
        }

        private static PropertyComparer<T> MakeComparer<T>(ListSortDescription sort)
        {
            return new PropertyComparer<T>(sort.PropertyDescriptor, sort.SortDirection);
        }
    }

    public class ListObjectComparer<T>
    {
        private readonly List<PropertyComparer<T>> _comparers;

        public ListObjectComparer(List<PropertyComparer<T>> comparers)
        {
            _comparers = comparers;
        }

        public int CompareValuesByProperties(T x, T y)
        {
            if (x == null)
                return (y == null) ? 0 : -1;

            if (y == null)
                return 1;

            foreach (PropertyComparer<T> comparer in _comparers)
            {
                int retval = comparer.Compare(x, y);
                if (retval != 0)
                    return retval;
            }

            return 0;
        }
    }
}
