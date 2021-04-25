using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using WinformsTools.MVVM.Telemetry;
using WinformsTools.MVVM.Tests.TestUtils;

namespace WinformsTools.MVVM.Tests.Telemetry
{
    [TestClass]
    public class ClickMetricTests
    {
        private AnyForm _view;
        private Button _button;
        private ClickMetric _metric;

        public class AnyForm : Form { }

        [TestInitialize]
        public void TestInitialize()
        {
            _view = new AnyForm();
            _button = new Button();
            _view.Controls.Add(_button);
            _metric = new ClickMetric(_button);
        }

        [TestMethod]
        public void Message_FormattedMessage()
        {
            // Arrange
            _button.Name = "AnyButtonName";

            // Act
            var message = SerilogFormatter.Format(_metric.MessageTemplate, _metric.MessageParams);

            // Assert
            message.Should().BeEquivalentTo("Control 'AnyButtonName' was clicked");
        }

        [TestMethod]
        public void Context_ViewName()
        {
            // Arrange
            _view.Name = "AnyFormName";

            // Assert
            _metric.Context.Should().Contain(key: "View", value: "AnyFormName");
        }
    }
}
