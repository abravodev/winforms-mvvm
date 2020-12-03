using AutoMapper;
using Easy.MessageHub;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserManager.BusinessLogic.DataAccess.Repositories;
using UserManager.BusinessLogic.Model;
using UserManager.DTOs;
using UserManager.Events;
using UserManager.Resources;
using UserManager.Startup;
using UserManager.Tests.TestUtils;
using UserManager.ViewModels;
using WinformsTools.MVVM.Components;

namespace UserManager.Tests.ViewModels
{
    [TestClass]
    public class CreateUserViewModelTests
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IMessageDialog _messageDialog;
        private IMapper _mapper;
        private IMessageHub _eventAggregator;
        private CreateUserViewModel sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _roleRepository = Substitute.For<IRoleRepository>();
            _messageDialog = Substitute.For<IMessageDialog>();
            _mapper = AutomapperConfig.GetMapperConfiguration().CreateMapper();
            _eventAggregator = Substitute.For<IMessageHub>();
            sut = new CreateUserViewModel(_userRepository, _roleRepository, _messageDialog, _mapper, _eventAggregator);
        }

        [TestMethod]
        public async Task Load_ShowListOfRolesInDropwdown()
        {
            // Arrange
            var role = Role.Basic;
            _roleRepository.GetAll().Returns(new List<Role> { role });

            // Act
            await sut.Load();

            // Assert
            sut.Roles.Should()
                .HaveCount(1)
                .And.ContainEquivalentOfMapped(role, _mapper);
        }

        [TestMethod]
        public async Task CanCreateUser_InvalidForm_ReturnsFalse()
        {
            // Arrange
            FillUserInfo(email: "invalid email");

            // Act
            var canCreateUser = sut.CanCreateUser;

            // Assert
            canCreateUser.Should().BeFalse();
        }

        [TestMethod]
        public async Task CanCreateUser_ValidForm_ReturnsTrue()
        {
            // Arrange
            FillUserInfo();

            // Act
            var canCreateUser = sut.CanCreateUser;

            // Assert
            canCreateUser.Should().BeTrue();
        }

        [TestMethod]
        public async Task CancelUserCreationCommand_UserInfoIsClearedFromForm()
        {
            // Arrange
            _roleRepository.GetAll().Returns(new List<Role> { Role.Basic, Role.Admin });
            await sut.Load();
            FillUserInfo(role: Role.Admin);

            // Act
            await sut.CancelUserCreationCommand.Execute();

            // Assert
            FormShouldBeEmpty();
        }

        [TestMethod]
        public async Task CreateUserCommand_InvalidForm_ShowErrorMessage()
        {
            // Arrange
            _roleRepository.GetAll().Returns(new List<Role> { Role.Basic, Role.Admin });
            await sut.Load();
            FillUserInfo(email: "invalid email");

            // Act
            await sut.CreateUserCommand.Execute();

            // Assert
            _messageDialog.Received()
                .Show(Arg.Is<ICollection<ValidationResult>>(x => x.Any(r => r.ErrorMessage == "Invalid Email: invalid email")));
            FormShouldBeEmpty();
        }

        [TestMethod]
        public async Task CreateUserCommand_ValidForm_CreateUser()
        {
            // Arrange
            _roleRepository.GetAll().Returns(new List<Role> { Role.Basic, Role.Admin });
            await sut.Load();
            FillUserInfo();
            var createdUserId = 1;
            var newUser = _mapper.Map<User>(sut.CreateUserInfo);
            _userRepository.CreateUser(_mapper.Map<User>(sut.CreateUserInfo)).Returns(createdUserId);

            // Act
            await sut.CreateUserCommand.Execute();

            // Assert
            await _userRepository.Received().CreateUser(ArgExt.AnyEquivalent(newUser));
            _eventAggregator.Received().Publish(ArgExt.AnyEquivalent(new UserCreatedEvent(newUser)));
            _messageDialog.Show(
                title: General.UserCreatedTitle,
                message: string.Format(General.UserCreatedMessage, newUser.FirstName, createdUserId));
            FormShouldBeEmpty();
        }

        [TestMethod]
        public async Task CreateUserCommand_ValidFormButSomethingFails_ShowErrorMessage()
        {
            // Arrange
            _roleRepository.GetAll().Returns(new List<Role> { Role.Basic, Role.Admin });
            await sut.Load();
            FillUserInfo();
            var exception = new Exception("Something failed");
            _userRepository
                .When(_ => _.CreateUser(Arg.Any<User>())).Throw(exception);

            // Act
            await sut.CreateUserCommand.Execute();

            // Assert
            _messageDialog.Received().ShowError(title: General.UserCreationFailedTitle, message: exception.Message);
            FormShouldBeEmpty();
        }

        private void FillUserInfo(
            string firstName = null, 
            string lastName = null, 
            string email = null, 
            Role role = null)
        {
            sut.CreateUserInfo.FirstName = firstName ?? "John";
            sut.CreateUserInfo.LastName = lastName ?? "Doe";
            sut.CreateUserInfo.Email = email ?? "john.doe@mail.com";
            sut.CreateUserInfo.Role = _mapper.Map<RoleSelectDto>(role ?? Role.Basic);
        }

        private void FormShouldBeEmpty()
        {
            var defaultRole = _mapper.Map<RoleSelectDto>(Role.Basic);
            sut.CreateUserInfo.FirstName.Should().BeEmpty();
            sut.CreateUserInfo.LastName.Should().BeEmpty();
            sut.CreateUserInfo.Email.Should().BeEmpty();
            sut.CreateUserInfo.Role.Should().BeEquivalentTo(defaultRole);
        }
    }
}
