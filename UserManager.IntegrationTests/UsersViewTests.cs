using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using UserManager.BusinessLogic.Model;
using System.Windows.Forms;
using UserManager.Common.Extensions;
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
            var userRows = usersView.FindFirstDescendant(x => x.ByControlType(ControlType.Table)).AsDataGridView().GetRows();
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
            var creationForm = usersView.FindFirstDescendant(x => x.ByName("Create User Form")).AsForm();
            var roleSelector = creationForm.FindFirstDescendant(x => x.ByControlType(ControlType.ComboBox).And(x.ByName("User role"))).AsComboBox();
            roleSelector.Value.Should().Be(defaultRole);
            CreateUser(usersView, user);

            var usersTableRows = usersView
                .FindFirstDescendant(x => x.ByControlType(ControlType.Table)).AsDataGridView()
                .GetRows().ToDictionary();
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
            var usersTable = usersView.FindFirstDescendant(x => x.ByControlType(ControlType.Table)).AsDataGridView().GetRows();
            if (usersTable.IsEmpty())
            {
                CreateRandomUser(usersView);
                usersTable = usersView.FindFirstDescendant(x => x.ByControlType(ControlType.Table)).AsDataGridView().GetRows();
            }

            var initialNumberOfUsers = usersTable.Length;
            var firstUser = usersTable.First();
            firstUser.RightClick();
            usersView.FindFirstDescendant(x => x.ByName("Context menu of Users list")).AsMenu().SelectMenuItem("Delete");
            usersView.GetModalByTitle("Delete user").AsModal().Choose(DialogResult.Yes);
            usersView.GetModalByTitle("User deleted").AsModal().Choose(DialogResult.OK);

            var finalNumberOfUsers = usersView.FindFirstDescendant(x => x.ByControlType(ControlType.Table)).AsDataGridView().GetRows().Length;
            finalNumberOfUsers.Should().BeLessThan(initialNumberOfUsers);
        }

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
            var creationForm = usersView.FindFirstDescendant(x => x.ByName("Create User Form")).AsForm();
            creationForm
                .Fill("First name", user.FirstName)
                .Fill("Last name", user.LastName)
                .Fill("Email", user.Email);
            creationForm.GetButtonByName("Save").Click();

            usersView.GetModalByTitle("User created").AsModal().Choose(DialogResult.OK);

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
            window.GetButtonByName("Users").Click();
            return App.GetUsersWindow();
        }
    }
}
