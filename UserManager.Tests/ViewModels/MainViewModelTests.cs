﻿using Easy.MessageHub;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinformsTools.MVVM.Components;
using WinformsTools.MVVM.Navigation;
using NSubstitute;
using System;
using UserManager.Providers;
using UserManager.Resources;
using UserManager.Tests.TestUtils;
using UserManager.ViewModels;
using UserManager.Views;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using FluentAssertions;
using UserManager.BusinessLogic.DataAccess;
using UserManager.DTOs;

namespace UserManager.Tests.ViewModels
{
    [TestClass]
    public class MainViewModelTests
    {
        private ISettingProvider _settingProvider;
        private IMessageDialog _messageDialog;
        private IViewNavigator _viewNavigator;
        private IMessageHub _eventAggregator;
        private IDatabaseService _databaseService;
        private MainViewModel sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _settingProvider = Substitute.For<ISettingProvider>();
            _messageDialog = Substitute.For<IMessageDialog>();
            _viewNavigator = Substitute.For<IViewNavigator>();
            _eventAggregator = Substitute.For<IMessageHub>();
            _databaseService = Substitute.For<IDatabaseService>();
            sut = CreateViewModel();
        }

        private MainViewModel CreateViewModel()
        {
            return new MainViewModel(_settingProvider, _messageDialog, _viewNavigator, _eventAggregator, _databaseService);
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
        public void NavigateToRolesView_OpenTheView()
        {
            // Act
            sut.NavigateToRolesView();

            // Assert
            _viewNavigator.Received().Open<RolesViewModel>();
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
            var viewModel = CreateViewModel();
            viewModel.NavigateToUsersView();
            onClosedEvent(ViewClosedEvent.Create<UsersView, UsersViewModel>());

            // Act
            viewModel.NavigateToUsersView();

            // Assert
            _viewNavigator.Received(2).Open<UsersViewModel>();
        }

        [TestMethod]
        public async Task Load_ShowMenuWithAvailableLanguages()
        {
            // Arrange
            var currentCulture = new CultureInfo("en");
            var otherCulture = new CultureInfo("es");
            _settingProvider.GetAvailableCultures().Returns(new List<CultureInfo> { currentCulture, otherCulture });
            _settingProvider.GetCurrentCulture().Returns(currentCulture);

            // Act
            await sut.Load();

            // Assert
            sut.AvailableLanguages.Should().HaveCount(2)
                .And.Contain(x => x.Culture == currentCulture && x.Current)
                .And.Contain(x => x.Culture == otherCulture && !x.Current);
        }

        [TestMethod]
        [DataRow("anyDatabase", "anyServer", true, ConnectionStatus.Connected)]
        [DataRow("anyDatabase", "anyServer", false, ConnectionStatus.Disconnected)]
        public async Task Load_ShowDatabaseConnectionStatus(string databaseName, string databaseServer, bool connected, ConnectionStatus status)
        {
            // Arrange
            _databaseService.GetName().Returns(databaseName);
            _databaseService.GetServer().Returns(databaseServer);
            _databaseService.CanConnectToDatabase().Returns(connected);
            _settingProvider.GetAvailableCultures().Returns(new List<CultureInfo>());

            // Act
            sut.DatabaseConnection.ConnectionStatus.Should().Be(ConnectionStatus.Connecting);
            await sut.Load();

            // Assert
            sut.DatabaseConnection.Name.Should().Be(databaseName);
            sut.DatabaseConnection.Server.Should().Be(databaseServer);
            sut.DatabaseConnection.ConnectionStatus.Should().Be(status);
        }

        [TestMethod]
        public async Task ChangeCurrentCulture_SameCultureThanSelected_DoNothing()
        {
            // Arrange
            var currentCulture = new CultureInfo("en");
            var otherCulture = new CultureInfo("es");
            _settingProvider.GetAvailableCultures().Returns(new List<CultureInfo> { currentCulture, otherCulture });
            _settingProvider.GetCurrentCulture().Returns(currentCulture);
            await sut.Load();
            var selectedLanguage = sut.AvailableLanguages.FirstOrDefault(x => x.Culture == currentCulture);

            // Act
            sut.ChangeCurrentCulture(selectedLanguage);

            // Assert
            _settingProvider.DidNotReceiveWithAnyArgs().SetCurrentCulture(null);
            _messageDialog.DidNotReceiveWithAnyArgs().Show(default(string), default(string));
        }

        [TestMethod]
        public async Task ChangeCurrentCulture_DifferentCultureThanSelected_SetNewCultureAndShowMessage()
        {
            // Arrange
            var currentCulture = new CultureInfo("en");
            var otherCulture = new CultureInfo("es");
            _settingProvider.GetAvailableCultures().Returns(new List<CultureInfo> { currentCulture, otherCulture });
            _settingProvider.GetCurrentCulture().Returns(currentCulture);
            await sut.Load();
            var newLanguage = sut.AvailableLanguages.FirstOrDefault(x => x.Culture == otherCulture);

            // Act
            sut.ChangeCurrentCulture(newLanguage);

            // Assert
            newLanguage.Checked.Should().BeTrue();
            newLanguage.Text.Should().Be(otherCulture.EnglishName);
            _settingProvider.Received().SetCurrentCulture(newLanguage.Culture);
            _messageDialog.Received().Show(
                title: General.LanguageChangeTitle,
                message: General.LanguageChangeMessage);
        }
    }
}
