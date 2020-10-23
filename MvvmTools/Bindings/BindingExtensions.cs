﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MvvmTools.Bindings
{
    public static class BindingExtensions
    {
        public static void AddBinding<TSource>(this DataGridView dataGridView, BindingList<TSource> items)
        {
            dataGridView.DataSource = new BindingSource(items, null);
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

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <paramref name="source"/>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="collection">The collection whose elements should be added to the end of the <paramref name="source"/></param>
        public static void AddRange<TSource>(this BindingList<TSource> source, IEnumerable<TSource> collection)
        {
            foreach (var item in collection)
            {
                source.Add(item);
            }
        }
    }
}