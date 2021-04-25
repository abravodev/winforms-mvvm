using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformsTools.MVVM.Core;
using WinformsTools.MVVM.Telemetry;
using WinformsTools.MVVM.Tests.TestUtils;

namespace WinformsTools.MVVM.Tests.Telemetry
{
    [TestClass]
    public class ViewEnteredMetricTests
    {
        private AnyForm _view;
        private ViewEnteredMetric<AnyFormViewModel> _metric;

        public class AnyForm : Form, IView<AnyFormViewModel>
        {
            public AnyFormViewModel ViewModel => null;

            public void InitializeDataBindings() { }
        }

        public class AnyFormViewModel : IViewModel
        {
            public async Task Load() { }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _view = new AnyForm();
            _metric = new ViewEnteredMetric<AnyFormViewModel>(_view);
        }

        [TestMethod]
        public void Message_FormattedMessage()
        {
            // Arrange
            _view.Name = "AnyFormName";

            // Act
            var message = SerilogFormatter.Format(_metric.MessageTemplate, _metric.MessageParams);

            // Assert
            message.Should().BeEquivalentTo("User entered view 'AnyFormName'");
        }

        
        [TestMethod]
        public void Context_Empty()
        {
            // Assert
            _metric.Context.Should().BeEmpty();
        }
    }
}
