using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace WinformsTools.MVVM.Bindings
{
    public class ConversionBinding<TSource, TDestination>
    {
        private readonly string _propertyName;
        private readonly object _dataSource;
        private readonly string _dataMember;
        private readonly SourceToControlConverter<TSource, TDestination> _converter;

        public ConversionBinding(
            string propertyName,
            object dataSource,
            string dataMember,
            SourceToControlConverter<TSource, TDestination> converter)
        {
            _propertyName = propertyName;
            _dataSource = dataSource;
            _dataMember = dataMember;
            _converter = converter;
        }

        public static implicit operator Binding(ConversionBinding<TSource, TDestination> cb)
        {
            var binding = new Binding(cb._propertyName, cb._dataSource, cb._dataMember, false, DataSourceUpdateMode.OnPropertyChanged);
            binding.Parse += (sender, e) => cb.ConvertValue(cb._converter, e);
            binding.Format += (sender, e) => cb.ConvertValue(cb._converter, e);
            return binding;
        }

        private void ConvertValue(SourceToControlConverter<TSource, TDestination> converter, ConvertEventArgs e)
        {
            e.Value = converter.Convert((TSource)e.Value);
        }
    }

    public class ConversionBind<TBinding, TControl, TProperty> : Bind<TBinding> where TControl : Control
    {
        private readonly TControl _control;
        private readonly Expression<Func<TControl, TProperty>> _controlProperty;

        public ConversionBind(
            TBinding item,
            TControl control,
            Expression<Func<TControl, TProperty>> controlProperty) : base(item)
        {
            _control = control;
            _controlProperty = controlProperty;
        }

        public Bind<TBinding> WithConverter<TSource>(
            Expression<Func<TBinding, TSource>> member,
            SourceToControlConverter<TSource, TProperty> converter)
        {
            var propertyName = ReflectionUtils.GetPropertyName(_controlProperty);
            var dataMember = ReflectionUtils.GetFullPath(member);
            var binding = new ConversionBinding<TSource, TProperty>(propertyName, _item, dataMember, converter);
            _control.DataBindings.Add(binding);
            return this;
        }

        public Bind<TBinding> WithConverter<TSource>(
            Expression<Func<TBinding, TSource>> member,
            Func<TSource, TProperty> converter)
        {
            var funcConverter = new FuncConverter<TSource, TProperty>(converter);
            return WithConverter(member, funcConverter);
        }

        private class FuncConverter<TSource, TDestination> : SourceToControlConverter<TSource, TDestination>
        {
            private readonly Func<TSource, TDestination> _converter;

            public FuncConverter(Func<TSource, TDestination> converter)
            {
                _converter = converter;
            }

            public override TDestination Convert(TSource source) => _converter(source);
        }
    }

    public abstract class SourceToControlConverter<TSource, TDestination>
    {
        public abstract TDestination Convert(TSource source);
    }
}
