using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using UserManager.BusinessLogic.DataAccess.Mappers;
using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.Tests.DataAccess.Mappers
{
    [TestClass]
    public class RoleMapperTests
    {
        [TestMethod]
        public void Parse_Null_ReturnNull()
        {
            // Arrange
            var mapper = new RoleMapper();

            // Act
            var role = mapper.Parse(null);

            // Assert
            role.Should().BeNull();
        }

        [TestMethod]
        public void Parse_ValidRole_ReturnParsedRole()
        {
            // Arrange
            var validRole = Role.Basic;
            var mapper = new RoleMapper();

            // Act
            var role = mapper.Parse(validRole.Id);

            // Assert
            role.Should().Be(validRole);
        }

        [TestMethod]
        public void Parse_InvalidEmail_ThrowException()
        {
            // Arrange
            var roleId = 0;
            var mapper = new RoleMapper();

            // Act
            Action action = () => mapper.Parse(roleId);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void SetValue_AnyRole_SetRoleIdToParameter()
        {
            // Arrange
            var validRole = Role.Basic;
            var mapper = new RoleMapper();
            var parameter = new SqlParameter();

            // Act
            mapper.SetValue(parameter, validRole);

            // Assert
            parameter.Value.Should().Be(validRole.Id);
        }

        [TestMethod]
        public void SetValue_Null_SetNullToParameter()
        {
            // Arrange
            var mapper = new RoleMapper();
            var parameter = new SqlParameter();

            // Act
            mapper.SetValue(parameter, null);

            // Assert
            parameter.Value.Should().Be(DBNull.Value);
        }
    }
}
