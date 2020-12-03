using FlaUI.Core.AutomationElements;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UserManager.BusinessLogic.Model;
using MvvmTools.IntegrationTestUtils.Extensions;
using MvvmTools.IntegrationTestUtils.Elements;

namespace UserManager.IntegrationTests.Users
{
    [TestClass]
    public class UserCreationTests : BaseUserView
    {
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
    }
}
