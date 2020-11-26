using Easy.MessageHub;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmTools.Components;
using MvvmTools.Navigation;
using NSubstitute;
using System;
using UserManager.Providers;
using UserManager.Resources;
using UserManager.Tests.TestUtils;
using UserManager.ViewModels;
using UserManager.Views;

namespace UserManager.Tests.ViewModels
{
    [TestClass]
    public class MainViewModelTests
    {
        private ISettingProvider _settingProvider;
        private IMessageDialog _messageDialog;
        private IViewNavigator _viewNavigator;
        private IMessageHub _eventAggregator;
        private MainViewModel sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _settingProvider = Substitute.For<ISettingProvider>();
            _messageDialog = Substitute.For<IMessageDialog>();
            _viewNavigator = Substitute.For<IViewNavigator>();
            _eventAggregator = Substitute.For<IMessageHub>();
            sut = new MainViewModel(_settingProvider, _messageDialog, _viewNavigator, _eventAggregator);
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

        [TestMethod]
        public void NavigateToUsersView_AfterOpeningAndClosingIt_OpenTheView()
        {
            // Arrange
            Action<ViewClosedEvent> onClosedEvent = null;
            _eventAggregator.GetAction<ViewClosedEvent>(action => onClosedEvent = action);
            var viewModel = new MainViewModel(_settingProvider, _messageDialog, _viewNavigator, _eventAggregator);
            viewModel.NavigateToUsersView();
            onClosedEvent(ViewClosedEvent.Create<UsersView, UsersViewModel>());

            // Act
            viewModel.NavigateToUsersView();

            // Assert
            _viewNavigator.Received(2).Open<UsersViewModel>();
        }
    }
}
