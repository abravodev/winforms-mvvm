using FlaUI.Core.AutomationElements;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinformsTools.IntegrationTestUtils.Extensions;
using WinformsTools.IntegrationTestUtils.Elements;
using UserManager.IntegrationTests.TestUtils;
using UserManager.Resources;

namespace UserManager.IntegrationTests.Main
{
    [TestClass]
    public class MainMenuTests : TestBase
    {
        [TestMethod]
        public void MainView_only_allows_to_open_one_windows_type_at_the_same_time()
        {
            var mainWindow = App.GetMainWindow();
            var gotoUsersButton = mainWindow.Get<Button>(General.Users);
            gotoUsersButton.Click();
            var usersView = App.GetUsersWindow();
            usersView.Should().NotBeNull();

            mainWindow.Focus();
            mainWindow.Get<Button>(General.Roles).Click();

            mainWindow.Focus();
            gotoUsersButton.Click();
            var errorModal = mainWindow.GetModalByTitle(General.AttentionTitle);
            errorModal.GetMessage().Should().Contain(General.CannotOpenWindowsTwice);

            errorModal.Choose(DialogOption.OK);
            usersView.Close();
            mainWindow.Focus();
            gotoUsersButton.Click();
            App.GetUsersWindow().Should().NotBeNull();
        }
    }
}
