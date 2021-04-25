using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using WinformsTools.MVVM.Controls;

namespace WinformsTools.MVVM.Tests.Controls
{
    [TestClass]
    public class ControlExtensionsTests
    {
        public class AnyForm : Form
        {

        }

        [TestMethod]
        public void GetView_ControlThatIsAForm_ReturnThatControl()
        {
            // Arrange
            var formControl = new AnyForm();

            // Act
            var form = formControl.GetView();

            // Assert
            form.Should().BeEquivalentTo(formControl);
        }

        [TestMethod]
        public void GetView_ControlWhoseParentIsAForm_ReturnParent()
        {
            // Arrange
            var formControl = new AnyForm();
            var button = new Button();
            formControl.Controls.Add(button);

            // Act
            var form = button.GetView();

            // Assert
            form.Should().BeEquivalentTo(button.Parent);
        }
    }
}
