using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace WinformsTools.MVVM.Bindings
{
    public class Bind<TBinding>
    {
        internal readonly TBinding _item;

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

        public ConversionBind<TBinding, TControl, TProperty> For<TControl, TProperty>(TControl control, Expression<Func<TControl, TProperty>> controlProperty)
            where TControl : Control
        {
            return new ConversionBind<TBinding, TControl, TProperty>(_item, control, controlProperty);
        }

        public Bind<TBinding> For<TControl, TProperty>(TControl control, Expression<Func<TControl, TProperty>> controlProperty, Expression<Func<TBinding, TProperty>> member)
            where TControl : Control
        {
            control.DataBindings.Add(ReflectionUtils.GetPropertyName(controlProperty), _item, ReflectionUtils.GetFullPath(member));
            return this;
        }

        public Bind<TBinding> For<TControl, TProperty>(
            TControl control,
            Expression<Func<TControl, TProperty>> controlProperty,
            Expression<Func<TBinding, TProperty>> member,
            Func<TBinding, BindableObject> dependsOn)
            where TControl : Control
        {
            var binding = control.DataBindings.Add(ReflectionUtils.GetPropertyName(controlProperty), _item, ReflectionUtils.GetPropertyName(member));
            dependsOn(_item).PropertyChanged += (sender, args) => binding.ReadValue();
            return this;
        }

        public Bind<TBinding> For<TProperty, TDisplayProperty, TValueProperty>(
            ComboBox comboBox,
            Func<TBinding, BindingList<TProperty>> member,
            Expression<Func<TProperty, TDisplayProperty>> displayMember,
            Expression<Func<TProperty, TValueProperty>> valueMember)
        {
            comboBox.DataSource = member(_item);
            comboBox.DisplayMember = ReflectionUtils.GetPropertyName(displayMember);
            comboBox.ValueMember = ReflectionUtils.GetPropertyName(valueMember);
            return this;
        }

        public Bind<TBinding> For<TSource, TProperty>(
            ComboBox comboBox,
            Expression<Func<TBinding, TProperty>> member,
            ComboBoxSource.ComboBoxBinding<TSource> binding)
        {
            comboBox.Bind(_item, member, binding);
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
            textBox.AddBinding(_item, ReflectionUtils.GetPropertyName(member));
            return this;
        }

        public Bind<TBinding> WithLoading<TControl>(TControl topControl, ProgressBar progressBar, Expression<Func<TBinding, bool>> member)
            where TControl : Control
        {
            For(progressBar, _ => _.Visible, member);
            topControl.AddInverseBinding(nameof(topControl.Enabled), _item, ReflectionUtils.GetPropertyName(member));
            return this;
        }

        public Bind<TBinding> WithTooltipOn<TControl>(TControl control, Func<TBinding, string> member, Func<TBinding, BindableObject> dependsOn)
            where TControl : Control
        {
            var tooltip = new ToolTip();
            dependsOn(_item).PropertyChanged += (sender, args) => tooltip.SetToolTip(control, member(_item));
            return this;
        }
    }
}
