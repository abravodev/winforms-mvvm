using System.Windows.Forms;

namespace WinformsTools.MVVM.Tests.TestUtils
{
    public static class DataGridViewExtensions
    {
        /// <summary>
        /// In order to use DataGridView bindings, you first need to call this method
        /// <see href="https://stackoverflow.com/questions/8340787/databinding-a-datagridview-control-which-is-not-in-form-controls-collection/8355131#8355131"/>
        /// </summary>
        /// <param name="view"></param>
        public static void PrepareForUnitTests(this DataGridView view)
        {
            view.BindingContext = new BindingContext();
        }
    }
}
