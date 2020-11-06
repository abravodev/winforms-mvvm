using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UserManager.IntegrationTests.TestUtils;
using UserManager.IntegrationTests.TestUtils.Extensions;

namespace UserManager.IntegrationTests
{
    [TestClass]
    public class UsersViewTests : TestBase
    {
        [TestMethod]
        public void User_can_view_a_list_of_users()
        {
            var usersView = NavigatetoUsersView();
            var usersTable = usersView.FindFirstDescendant(x => x.ByControlType(ControlType.Table)).AsDataGridView();
            Retry.WhileEmpty(() => usersTable.Rows);
            usersTable.Rows.Should().NotBeEmpty();
        }

        [TestMethod]
        public void User_can_create_a_new_user()
        {
            var usersView = NavigatetoUsersView();
            var creationForm = usersView.FindFirstDescendant(x => x.ByName("Create User Form")).AsForm();
            var user = new
            {
                FirstName = "John",
                LastName = "Doe",
                Email = $"john.doe_{DateTime.Now.Ticks}@mail.com"
            };
            creationForm
                .Fill("First name", user.FirstName)
                .Fill("Last name", user.LastName)
                .Fill("Email", user.Email);
            creationForm.GetButtonByName("Save").Click();

            var userCreatedModal = usersView.GetModalByTitle("User created");
            userCreatedModal.GetFirstButton().Click();

            var usersTable = usersView.FindFirstDescendant(x => x.ByControlType(ControlType.Table)).AsDataGridView();
            Retry.WhileEmpty(() => usersTable.Rows);
            var rows = usersTable.ToDictionary();
            rows.Should().Contain(row =>
                row["First name"].Value == user.FirstName &&
                row["Last name"].Value == user.LastName &&
                row["Email"].Value == user.Email);
        }

        private Window NavigatetoUsersView()
        {
            var window = App.GetMainWindow();
            window.GetButtonByName("Users").Click();
            return App.GetUsersWindow();
        }
    }
}
