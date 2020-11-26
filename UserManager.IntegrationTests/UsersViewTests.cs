using FlaUI.Core.AutomationElements;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using UserManager.BusinessLogic.Model;
using UserManager.Common.Extensions;
using UserManager.IntegrationTests.TestUtils;
using UserManager.IntegrationTests.TestUtils.Extensions;
using UserManager.IntegrationTests.TestUtils.Elements;
using FlaUI.Core.Definitions;

namespace UserManager.IntegrationTests
{
    [TestClass]
    public class UsersViewTests : TestBase
    {
        [TestMethod]
        public void User_can_view_a_list_of_users()
        {
            var usersView = NavigatetoUsersView();
            var userRows = GetUserRows(usersView);
            userRows.Should().NotBeEmpty();
        }

        [TestMethod]
        public void User_can_create_a_new_user()
        {
            var usersView = NavigatetoUsersView();
            var user = new UserInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Email = $"john.doe_{DateTime.Now.Ticks}@mail.com"
            };
            var defaultRole = Role.Basic.Name;
            var creationForm = usersView.Get<VisualForm>("Create User Form");
            var roleSelector = creationForm.Get<ComboBox>("User role");
            roleSelector.Value.Should().Be(defaultRole);
            CreateUser(usersView, user);

            var usersTableRows = GetUserRows(usersView).ToDictionary();
            usersTableRows.Should().Contain(row =>
                row["First name"].Value == user.FirstName &&
                row["Last name"].Value == user.LastName &&
                row["Email"].Value == user.Email &&
                row["Role"].Value == defaultRole);
        }

        [TestMethod]
        public void User_can_delete_an_existing_user()
        {
            var usersView = NavigatetoUsersView();
            var usersTable = GetUserRows(usersView);
            if (usersTable.IsEmpty())
            {
                CreateRandomUser(usersView);
                usersTable = GetUserRows(usersView);
            }

            var initialNumberOfUsers = usersTable.Length;
            var firstUser = usersTable.First();
            firstUser.RightClick();
            usersView.Get<Menu>("Context menu of Users list", ControlType.ToolBar).SelectMenuItem("Delete");
            usersView.GetModalByTitle("Delete user").Choose(DialogOption.Yes);
            usersView.GetModalByTitle("User deleted").Choose(DialogOption.OK);

            var finalNumberOfUsers = GetUserRows(usersView).Length;
            finalNumberOfUsers.Should().BeLessThan(initialNumberOfUsers);
        }

        private static DataGridViewRow[] GetUserRows(Window usersView) => usersView.Get<DataGridView>("Users list").GetRows();

        private UserInfo CreateRandomUser(Window usersView)
        {
            var user = new UserInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Email = $"john.doe_{DateTime.Now.Ticks}@mail.com"
            };
            return CreateUser(usersView, user);
        }

        private UserInfo CreateUser(Window usersView, UserInfo user)
        {
            var creationForm = usersView.Get<VisualForm>("Create User Form");
            creationForm
                .Fill("First name", user.FirstName)
                .Fill("Last name", user.LastName)
                .Fill("Email", user.Email);
            creationForm.Get<Button>("Save").Click();

            usersView.GetModalByTitle("User created").Choose(DialogOption.OK);

            return user;
        }

        private class UserInfo
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }
        }

        private Window NavigatetoUsersView()
        {
            var window = App.GetMainWindow();
            window.Get<Button>("Users").Click();
            return App.GetUsersWindow();
        }
    }
}
