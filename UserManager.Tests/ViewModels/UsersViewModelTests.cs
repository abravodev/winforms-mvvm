using AutoMapper;
using Easy.MessageHub;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserManager.BusinessLogic.DataAccess.Repositories;
using UserManager.BusinessLogic.Model;
using UserManager.Events;
using UserManager.Resources;
using UserManager.Startup;
using UserManager.Tests.TestUtils;
using UserManager.Tests.TestUtils.Fakes;
using UserManager.ViewModels;
using WinformsTools.MVVM.Components;
using WinformsTools.MVVM.Controls.SnackbarControl;

namespace UserManager.Tests.ViewModels
{
    [TestClass]
    public class UsersViewModelTests
    {
        private IUserRepository _userRepository;
        private IMessageDialog _messageDialog;
        private IMapper _mapper;
        private IMessageHub _eventAggregator;
        private ISnackbarMessage _snackbarMessage;
        private ISnackbarProvider _snackbarProvider;
        private UsersViewModel sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _messageDialog = Substitute.For<IMessageDialog>();
            _mapper = AutomapperConfig.GetMapperConfiguration().CreateMapper();
            _eventAggregator = Substitute.For<IMessageHub>();
            var snackbar = FakeSnackbarMessage.Create();
            _snackbarMessage = snackbar.Message;
            _snackbarProvider = snackbar.Provider;
            sut = CreateUserViewModel();
        }

        private UsersViewModel CreateUserViewModel()
        {
            return new UsersViewModel(_userRepository, _messageDialog, _mapper, _eventAggregator, _snackbarProvider);
        }

        [TestMethod]
        public async Task Load_ShowListOfUsers()
        {
            // Arrange
            var user = MakeUser();
            _userRepository.GetAll().Returns(new List<User> { user });
            var listener = PropertyChangeListener.Start(sut);

            // Act
            await sut.Load();

            // Assert
            var changes = listener.Stop().GetChanges<bool>(nameof(sut.Loading));
            sut.Users.Should()
                .HaveCount(1)
                .And.ContainEquivalentOfMapped(user, _mapper);
            changes.Should().Contain(x => x.Value == true);
        }

        [TestMethod]
        public async Task Load_ErrorWhileRetrievingUsers_ShowErrorMessage()
        {
            // Arrange
            var exception = new Exception("Error while retrieving users");
            _userRepository.When(_ => _.GetAll()).Throw(exception);

            // Act
            await sut.Load();

            // Assert
            _messageDialog.Received().ShowError(title: General.ErrorLoadingUsersTitle, message: exception.Message);
        }

        [TestMethod]
        public async Task UserIsCreated_AddToUserListAndShowSnackbarMessage()
        {
            // Arrange
            var createdUser = MakeUser();
            Action<UserCreatedEvent> onUserCreatedEvent = null;
            _eventAggregator.GetAction<UserCreatedEvent>(action => onUserCreatedEvent = action);
            var viewModel = CreateUserViewModel(); // Needed because it subscribes to the events in the constructor
            onUserCreatedEvent(new UserCreatedEvent(createdUser));

            // Act
            await viewModel.Load();

            // Assert
            viewModel.Users.Should()
                .HaveCount(1)
                .And.ContainEquivalentOfMapped(createdUser, _mapper);
            _snackbarMessage.Received().Show(
                message: string.Format(General.UserCreatedMessage, createdUser.FirstName, createdUser.Id));
        }

        [TestMethod]
        public async Task DeleteUser_ConfirmationIsCanceled_UserIsNitDeleted()
        {
            // Arrange
            var user = MakeUser();
            _userRepository.GetAll().Returns(new List<User> { user });
            await sut.Load();
            var userToDelete = sut.Users.First();
            _messageDialog
                .Show(
                    title: General.DeleteUserTitle,
                    message: string.Format(General.DeleteUserQuestion, userToDelete.Fullname),
                    buttons: MessageBoxButtons.YesNo,
                    icon: MessageBoxIcon.Warning)
                .Returns(DialogResult.No);

            // Act
            await sut.DeleteUserCommand.Execute(userToDelete);

            // Assert
            await _userRepository.DidNotReceive().Remove(user.Id);
            sut.Users.Should().HaveCount(1);
            _snackbarMessage.DidNotReceive().Show(
                message: string.Format(General.UserDeletedMessage, userToDelete.Fullname));
        }

        [TestMethod]
        public async Task DeleteUser_ConfirmationIsAccepted_UserIsDeleted()
        {
            // Arrange
            var user = MakeUser();
            _userRepository.GetAll().Returns(new List<User> { user });
            await sut.Load();
            var userToDelete = sut.Users.First();
            _messageDialog
                .Show(
                    title: General.DeleteUserTitle,
                    message: string.Format(General.DeleteUserQuestion, userToDelete.Fullname),
                    buttons: MessageBoxButtons.YesNo,
                    icon: MessageBoxIcon.Warning)
                .Returns(DialogResult.OK);

            // Act
            await sut.DeleteUserCommand.Execute(userToDelete);

            // Assert
            await _userRepository.Received().Remove(user.Id);
            sut.Users.Should().BeEmpty();
            _snackbarMessage.Received().Show(
                message: string.Format(General.UserDeletedMessage, userToDelete.Fullname));
        }

        private static User MakeUser()
        {
            return new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = new Email("john.doe@mail.com"),
                Role = Role.Basic
            };
        }
    }
}
