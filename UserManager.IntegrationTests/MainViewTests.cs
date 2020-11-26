using FlaUI.Core.AutomationElements;
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

        [TestMethod]
        public void MainView_only_allows_to_open_one_windows_type_at_the_same_time()
        {
            var mainWindow = App.GetMainWindow();
            var gotoUsersButton = mainWindow.Get<Button>("Users");
            gotoUsersButton.Click();
            var usersView = App.GetUsersWindow();
            usersView.Should().NotBeNull();

            mainWindow.Focus();
            mainWindow.Get<Button>("Roles").Click();

            mainWindow.Focus();
            gotoUsersButton.Click();
            var errorModal = mainWindow.GetModalByTitle("Attention!");
            errorModal.GetMessage().Should().Contain("Cannot open same window");

            errorModal.Choose(DialogOption.OK);
            usersView.Close();
            mainWindow.Focus();
            gotoUsersButton.Click();
            App.GetUsersWindow().Should().NotBeNull();
        }
    }
}
