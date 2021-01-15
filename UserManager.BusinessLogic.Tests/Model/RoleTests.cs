using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.Tests.Model
{
    [TestClass]
    public class RoleTests
    {
        [TestMethod] 
        public void FromId_WrongId_ThrowException()
        {
            // Arrange
            var wrongRoleId = 0;

            // Act
            Action action = () => Role.FromId(wrongRoleId);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void FromId_ExistingId_ReturnRole(int roleId)
        {
            // Act
            var role = Role.FromId(roleId);

            // Assert
            role.Id.Should().Be(roleId);
        }

        [TestMethod]
        public void Roles_ReturnTheListOfRoles()
        {
            // Act
            var roles = Role.Roles;

            // Assert
            roles.Should().BeEquivalentTo(new[] { Role.Admin, Role.Basic });
        }
    }
}
