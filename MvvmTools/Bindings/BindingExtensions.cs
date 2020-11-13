﻿using System.Windows.Forms;

namespace MvvmTools.Bindings
{
    public static class BindingExtensions
    {
        public static void AddBinding<TSource>(this TextBox textBox, TSource dataSource, string dataMember)
        {
            textBox.DataBindings.Add(
                propertyName: nameof(textBox.Text),
                dataSource: dataSource,
                dataMember: dataMember,
                formattingEnabled: false,
                updateMode: DataSourceUpdateMode.OnPropertyChanged);
        }

        public static void AddInverseBinding<TControl>(this TControl control, string propertyName, object dataSource, string dataMember) 
            where TControl : Control
        {
            control.DataBindings.Add(new InverseBinding(propertyName, dataSource, dataMember));
        }

        public static Bind<TItem> BindTo<TView, TItem>(this TView view, TItem item)
            where TView : ContainerControl
        {
            return new Bind<TItem>(item);
        }
    }
}
