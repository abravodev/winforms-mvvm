using System.Windows.Forms;

namespace WinformsTools.MVVM.Bindings
{
    /// <summary>
    /// <see href="https://stackoverflow.com/a/19718906"/>
    /// </summary>
    public class InverseBinding
    {
        private readonly string _propertyName;
        private readonly object _dataSource;
        private readonly string _dataMember;

        public InverseBinding(string propertyName, object dataSource, string dataMember)
        {
            _propertyName = propertyName;
            _dataSource = dataSource;
            _dataMember = dataMember;
        }

        public static implicit operator Binding(InverseBinding eb)
        {
            var binding = new Binding(eb._propertyName, eb._dataSource, eb._dataMember, false, DataSourceUpdateMode.OnPropertyChanged);
            binding.Parse += InvertValue;
            binding.Format += InvertValue;
            return binding;
        }

        private static void InvertValue(object sender, ConvertEventArgs e) => e.Value = !((bool)e.Value);
    }
}
