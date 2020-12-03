using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace WinformsTools.MVVM.Bindings
{
    public static class ComboBoxSource
    {
        public static ComboBoxBinding<TSource> From<TSource>(BindingList<TSource> source)
        {
            return new ComboBoxBinding<TSource>(source);
        }

        public class ComboBoxBinding<TSource>
        {
            public BindingList<TSource> Source { get; }

            public string DisplayMember { get; private set; }

            public string ValueMember { get; private set; }

            public Func<TSource, bool> DefaultValue { get; private set; }

            public ComboBoxBinding(BindingList<TSource> source)
            {
                Source = source;
            }

            public ComboBoxBinding<TSource> Display<TDisplayProperty>(Expression<Func<TSource, TDisplayProperty>> displayMember)
            {
                DisplayMember = ReflectionUtils.GetPropertyName(displayMember);
                return this;
            }

            public ComboBoxBinding<TSource> Value<TValueProperty>(Expression<Func<TSource, TValueProperty>> valueMember)
            {
                ValueMember = ReflectionUtils.GetPropertyName(valueMember);
                return this;
            }

            public ComboBoxBinding<TSource> Default(Func<TSource, bool> value)
            {
                DefaultValue = value;
                return this;
            }

            public ComboBoxBinding<TSource> On<TDisplayProperty, TValueProperty>(
                Expression<Func<TSource, TDisplayProperty>> displayMember,
                Expression<Func<TSource, TValueProperty>> valueMember)
            {
                Display(displayMember);
                Value(valueMember);
                return this;
            }
        }
    }
}
