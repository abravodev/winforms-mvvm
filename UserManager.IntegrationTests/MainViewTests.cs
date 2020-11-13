using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using UserManager.IntegrationTests.TestUtils;
using UserManager.IntegrationTests.TestUtils.Extensions;

namespace UserManager.IntegrationTests
{
    [TestClass]
    public class MainViewTests : TestBase
    {
        [TestMethod]
        public void MainView_has_a_main_menu_with_options()
        {
            var window = App.GetMainWindow();
            window.Title.Should().Contain("UserManager");

            window.GetButtonByName("Users").Should().NotBeNull();
            window.GetButtonByName("Roles").Should().NotBeNull();
        }

        [TestMethod]
        public void MainView_has_a_menu_with_options()
        {
            var window = App.GetMainWindow();

            var settingsOption = window.FindFirstDescendant(x => x.ByControlType(ControlType.MenuItem).And(x.ByName("Settings"))).AsMenuItem();
            var languageOption = settingsOption.Items.First(x => x.Name.Contains("Language"));

            languageOption.Should().NotBeNull();
            languageOption.Items.Should()
                .Contain(x => x.Text == "Spanish")
                .And.Contain(x => x.Text == "English");
        }
    }
}
