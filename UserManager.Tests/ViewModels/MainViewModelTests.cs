using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmTools.Components;
using NSubstitute;
using UserManager.Providers;
using UserManager.Resources;
using UserManager.ViewModels;

namespace UserManager.Tests.ViewModels
{
    [TestClass]
    public class MainViewModelTests
    {
        private ISettingProvider _settingProvider;
        private IMessageDialog _messageDialog;
        private IViewNavigator _viewNavigator;
        private MainViewModel sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _settingProvider = Substitute.For<ISettingProvider>();
            _messageDialog = Substitute.For<IMessageDialog>();
            _viewNavigator = Substitute.For<IViewNavigator>();
            sut = new MainViewModel(_settingProvider, _messageDialog, _viewNavigator);
        }

        [TestMethod]
        public void NavigateToUsersView_FirstTime_OpenTheView()
        {
            // Act
            sut.NavigateToUsersView();

            // Assert
            _viewNavigator.Received().Open<UsersViewModel>();
        }

        [TestMethod]
        public void NavigateToUsersView_AlreadyOpened_ShowErrorMessage()
        {
            // Arrange
            sut.NavigateToUsersView();
            _viewNavigator.ClearReceivedCalls();
            
            // Act
            sut.NavigateToUsersView();

            // Assert
            _viewNavigator.DidNotReceive().Open<UsersViewModel>();
            _messageDialog.Received().ShowError(
                    title: General.AttentionTitle,
                    message: General.CannotOpenWindowsTwice);
        }
    }
}
