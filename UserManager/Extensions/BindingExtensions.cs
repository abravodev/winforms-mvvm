using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace UserManager.Extensions
{
    public static class BindingExtensions
    {
        public static void AddBinding<TSource>(this DataGridView dataGridView, IList<TSource> items)
        {
            var bindingList = new BindingList<TSource>(items);
            dataGridView.DataSource = new BindingSource(bindingList, null);
        }
    }
}
