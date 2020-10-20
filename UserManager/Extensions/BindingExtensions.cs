﻿using System.Collections.Generic;
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

        public static void ResetDataSourceBinding(this DataGridView dataGridView)
        {
            (dataGridView.DataSource as BindingSource).ResetBindings(false);
        }

        public static void AddBinding<TSource>(this TextBox textBox, TSource dataSource, string dataMember)
        {
            textBox.DataBindings.Add(
                propertyName: nameof(textBox.Text),
                dataSource: dataSource,
                dataMember: dataMember,
                formattingEnabled: false,
                updateMode: DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
