using FlaUI.Core.AutomationElements;
using UserManager.IntegrationTests.TestUtils;
using WinformsTools.IntegrationTestUtils.Extensions;
using WinformsTools.IntegrationTestUtils.Elements;

namespace UserManager.IntegrationTests.Users
{
    public class BaseUserView : TestBase
    {
        protected static DataGridViewRow[] GetUserRows(Window usersView) => usersView.Get<DataGridView>("Users list").GetRows();

        protected UserInfo CreateUser(Window usersView, UserInfo user)
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

        protected class UserInfo
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }
        }

        protected Window NavigatetoUsersView()
        {
            var window = App.GetMainWindow();
            window.Get<Button>("Users").Click();
            return App.GetUsersWindow();
        }
    }
}
