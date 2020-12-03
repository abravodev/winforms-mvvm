using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManager.BusinessLogic.DataAccess.Repositories;
using UserManager.BusinessLogic.Model;
using UserManager.Resources;
using UserManager.Startup;
using UserManager.Tests.TestUtils;
using UserManager.ViewModels;
using WinformsTools.MVVM.Components;

namespace UserManager.Tests.ViewModels
{
    [TestClass]
    public class RolesViewModelTests
    {
        private IRoleRepository _roleRepository;
        private IMessageDialog _messageDialog;
        private IMapper _mapper;
        private RolesViewModel sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _roleRepository = Substitute.For<IRoleRepository>();
            _messageDialog = Substitute.For<IMessageDialog>();
            _mapper = AutomapperConfig.GetMapperConfiguration().CreateMapper();
            sut = new RolesViewModel(_roleRepository, _messageDialog, _mapper);
        }

        [TestMethod]
        public async Task Load_ShowListOfRoles()
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
        public async Task Load_ErrorWhileRetrievingRoles_ShowErrorMessage()
        {
            // Arrange
            var exception = new Exception("Error while retrieving users");
            _roleRepository.When(_ => _.GetAll()).Throw(exception);

            // Act
            await sut.Load();

            // Assert
            _messageDialog.Received().ShowError(title: General.ErrorLoadingRolesTitle, message: exception.Message);
        }   
    }
}
