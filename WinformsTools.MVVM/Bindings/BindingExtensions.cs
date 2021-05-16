using System.Windows.Forms;

namespace WinformsTools.MVVM.Bindings
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
            var inverseBinding = new ConversionBinding<bool, bool>(
                propertyName,
                dataSource,
                dataMember,
                converter: new InverseBoolConverter());
            control.AddBinding(inverseBinding);
        }

        public static void AddBinding<TControl, TSource, TDestination>(
            this TControl control,
            ConversionBinding<TSource, TDestination> binding)
            where TControl : Control
        {
            control.DataBindings.Add(binding);
        }

        public static Bind<TItem> BindTo<TView, TItem>(this TView view, TItem item)
            where TView : ContainerControl
        {
            return new Bind<TItem>(item);
        }
    }
}
