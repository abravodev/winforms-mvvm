using FlaUI.Core.AutomationElements;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using UserManager.IntegrationTests.TestUtils;
using MvvmTools.IntegrationTestUtils.Extensions;

namespace UserManager.IntegrationTests.Main
{
    [TestClass]
    public class SettingsMenuTests : TestBase
    {
        [TestMethod]
        public void MainView_has_a_main_menu_with_options()
        {
            var window = App.GetMainWindow();
            window.Title.Should().Contain("UserManager");

            window.Get<Button>("Users").Should().NotBeNull();
            window.Get<Button>("Roles").Should().NotBeNull();
        }

        [TestMethod]
        public void MainView_has_a_menu_with_options()
        {
            var window = App.GetMainWindow();

            var settingsOption = window.Get<MenuItem>("Settings");
            var languageOption = settingsOption.Items.First(x => x.Name.Contains("Language"));

            languageOption.Should().NotBeNull();
            languageOption.Items.Should()
                .Contain(x => x.Text == "Spanish")
                .And.Contain(x => x.Text == "English");
        }
    }
}
