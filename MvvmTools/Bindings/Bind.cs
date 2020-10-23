using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace MvvmTools.Bindings
{
    public class Bind<TBinding>
    {
        private readonly TBinding _item;

        public Bind(TBinding item)
        {
            _item = item;
        }

        public Bind<TBinding> For<TControl>(TControl control, string propertyName, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode)
            where TControl : Control
        {
            control.DataBindings.Add(propertyName, _item, dataMember, formattingEnabled, updateMode);
            return this;
        }

        public Bind<TBinding> For<TDataGridView, TSource>(TDataGridView datagridView, Func<TBinding, BindingList<TSource>> items)
            where TDataGridView : DataGridView
        {
            datagridView.AddBinding(items(_item));
            return this;
        }

        public Bind<TBinding> For<TControl>(TControl control, Expression<Func<TControl, object>> controlProperty, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode)
            where TControl : Control
        {
            control.DataBindings.Add(GetPropertyName(controlProperty), _item, dataMember, formattingEnabled, updateMode);
            return this;
        }

        /// <summary>
        /// Binds <see cref="TextBox.Text"/> of <paramref name="textBox"/> to <paramref name="memberName"/>
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public Bind<TBinding> For(TextBox textBox, string memberName)
        {
            textBox.AddBinding(_item, memberName);
            return this;
        }

        /// <summary>
        /// Binds <see cref="TextBox.Text"/> of <paramref name="textBox"/> to <paramref name="member"/>
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public Bind<TBinding> For(TextBox textBox, Expression<Func<TBinding, object>> member)
        {
            return For(textBox, GetPropertyName(member));
        }

        private string GetPropertyName<T>(Expression<Func<T, object>> action)
        {
            var body = (MemberExpression)action.Body;
            return body.Member.Name;
        }
    }

    public static class BindExtensions
    {
        public static Bind<TItem> BindTo<TView, TItem>(this TView view, TItem item)
            where TView : ContainerControl
        {
            return new Bind<TItem>(item);
        }
    }
}
