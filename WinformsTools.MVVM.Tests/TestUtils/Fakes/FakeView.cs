using System.Windows.Forms;
using WinformsTools.MVVM.Core;

namespace WinformsTools.MVVM.Tests.TestUtils.Fakes
{
    public class FakeView : Form, IView<FakeViewModel>
    {
        public FakeViewModel ViewModel { get; }

        public void InitializeDataBindings() { }
    }
}
