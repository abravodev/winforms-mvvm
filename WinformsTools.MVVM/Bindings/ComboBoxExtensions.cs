using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace WinformsTools.MVVM.Bindings
{
    public static class ComboBoxExtensions
    {
        public static void Bind<TBinding, TSource, TProperty>(
            this ComboBox comboBox,
            TBinding item,
            Expression<Func<TBinding, TProperty>> member, ComboBoxSource.ComboBoxBinding<TSource> binding)
        {
            BindSource(comboBox, binding);
            BindSelectedItem(comboBox, item, member, binding);
        }

        private static void BindSelectedItem<TBinding, TSource, TProperty>(ComboBox comboBox, 
            TBinding item, 
            Expression<Func<TBinding, TProperty>> member, 
            ComboBoxSource.ComboBoxBinding<TSource> binding)
        {
            comboBox.DataBindings.Add(nameof(ComboBox.SelectedItem), item, ReflectionUtils.GetPropertyName(member));
            comboBox.SetDefault(binding.DefaultValue);
            comboBox.SelectionChangeCommitted += (sender, e) => comboBox.DataBindings[nameof(ComboBox.SelectedItem)].WriteValue();
            (comboBox.DataSource as IBindingList).ListChanged += (sender, e) =>
            {
                var value = ReflectionUtils.GetValue(item, member);
                if (value == null)
                {
                    comboBox.SetDefault(binding.DefaultValue);
                }
            };
        }

        private static void BindSource<TSource>(ComboBox comboBox, ComboBoxSource.ComboBoxBinding<TSource> binding)
        {
            comboBox.DataSource = binding.Source;
            comboBox.DisplayMember = binding.DisplayMember;
            comboBox.ValueMember = binding.ValueMember;
        }

        private static void SetDefault<TSource>(
            this ComboBox comboBox,
            Func<TSource, bool> defaultValuePredicate)
        {
            var defaultValue = GetDefaultItem(comboBox, defaultValuePredicate);
            if(defaultValue == null)
            {
                return;
            }

            comboBox.SelectedItem = defaultValue;
            comboBox.DataBindings[nameof(ComboBox.SelectedItem)].WriteValue();
        }

        private static object GetDefaultItem<TSource>(ComboBox comboBox, Func<TSource,bool> defaultValue)
        {
            foreach(var item in comboBox.Items)
            {
                if (defaultValue((TSource)item))
                {
                    return item;
                }
            }
            return null;
        }
    }
}
