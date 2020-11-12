﻿using MvvmTools.Controls.DataGridViewControl;
using MvvmTools.Core;
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

        public Bind<TBinding> For<TControl>(TControl control, string propertyName, string dataMember)
            where TControl : Control
        {
            control.DataBindings.Add(propertyName, _item, dataMember);
            return this;
        }

        public Bind<TBinding> For<TControl, TProperty>(TControl control, Expression<Func<TControl, TProperty>> controlProperty, Expression<Func<TBinding, TProperty>> member)
            where TControl : Control
        {
            control.DataBindings.Add(GetPropertyName(controlProperty), _item, GetPropertyName(member));
            return this;
        }

        public Bind<TBinding> WithLoading<TControl>(TControl topControl, ProgressBar progressBar, Expression<Func<TBinding, bool>> member)
            where TControl: Control
        {
            For(progressBar, _ => _.Visible, member);
            topControl.AddInverseBinding(nameof(topControl.Enabled), _item, GetPropertyName(member));
            return this;
        }

        public Bind<TBinding> For<TControl, TProperty>(TControl control, Expression<Func<TControl, TProperty>> controlProperty, Expression<Func<TBinding, TProperty>> member, Func<TBinding, BindableObject> dependsOn)
            where TControl : Control
        {
            var binding = control.DataBindings.Add(GetPropertyName(controlProperty), _item, GetPropertyName(member));
            dependsOn(_item).PropertyChanged += (sender, args) => binding.ReadValue();
            return this;
        }

        public Bind<TBinding> For<TDataGridView, TSource>(TDataGridView datagridView, Func<TBinding, BindingList<TSource>> items)
            where TDataGridView : DataGridView
        {
            datagridView.AddBinding(items(_item));
            return this;
        }

        public Bind<TBinding> For<TSource>(BindedAdvancedDataGridView datagridView, Func<TBinding, BindingList<TSource>> items)
        {
            var list = items(_item);
            datagridView.Bind(list);
            datagridView.AddBinding(list);
            return this;
        }

        public Bind<TBinding> WithContextMenu<TSource>(DataGridView dataGridView, params (string Name, ICommand<TSource> Command)[] menuItems)
        {
            dataGridView.WithContextMenu(menuItems);
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
        public Bind<TBinding> For(TextBox textBox, Expression<Func<TBinding, string>> member)
        {
            textBox.AddBinding(_item, GetPropertyName(member));
            return this;
        }

        public Bind<TBinding> Click(Control control, Action onClick)
        {
            control.Click += (sender, args) => onClick();
            return this;
        }

        public Bind<TBinding> Click(Control control, Action<object, EventArgs> onClick)
        {
            control.Click += (sender, args) => onClick(sender, args);
            return this;
        }

        public Bind<TBinding> Click(Control control, Func<TBinding, Core.ICommand> command)
        {
            control.Click += (sender, e) => command(_item).Execute();
            return this;
        }

        public Bind<TBinding> Click<TParameter>(Control control, Func<TBinding, Core.ICommand<TParameter>> command, Func<TParameter> parameter)
        {
            control.Click += (sender, e) => command(_item).Execute(parameter());
            return this;
        }

        private string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> action)
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
