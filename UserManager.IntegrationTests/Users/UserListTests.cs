using FlaUI.Core.AutomationElements;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using UserManager.Common.Extensions;
using WinformsTools.IntegrationTestUtils.Extensions;
using WinformsTools.IntegrationTestUtils.Elements;
using FlaUI.Core.Definitions;

namespace UserManager.IntegrationTests.Users
{
    [TestClass]
    public class UserListTests : BaseUserView
    {
        [TestMethod]
        public void User_can_view_a_list_of_users()
        {
            var usersView = NavigatetoUsersView();
            var userRows = GetUserRows(usersView);
            userRows.Should().NotBeEmpty();
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
    }
}
