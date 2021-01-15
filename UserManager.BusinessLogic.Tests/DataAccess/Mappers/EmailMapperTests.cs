using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using UserManager.BusinessLogic.DataAccess.Mappers;
using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.Tests.DataAccess.Mappers
{
    [TestClass]
    public class EmailMapperTests
    {
        [TestMethod]
        public void Parse_Null_ReturnNull()
        {
            // Arrange
            var mapper = new EmailMapper();

            // Act
            var email = mapper.Parse(null);

            // Assert
            email.Should().BeNull();
        }

        [TestMethod]
        public void Parse_ValidEmail_ReturnParsedEmail()
        {
            // Arrange
            var emailAddress = "valid_email@mail.com";
            var mapper = new EmailMapper();

            // Act
            var email = mapper.Parse(emailAddress);

            // Assert
            email.ToString().Should().Be(emailAddress);
        }

        [TestMethod]
        public void Parse_InvalidEmail_ThrowException()
        {
            // Arrange
            var emailAddress = "invalid_email";
            var mapper = new EmailMapper();

            // Act
            Action action = () => mapper.Parse(emailAddress);

            // Assert
            action.Should().Throw<ValidationException>();
        }

        [TestMethod]
        public void SetValue_AnyEmail_SetEmailToParameter()
        {
            // Arrange
            var emailAddress = "valid_email@mail.com";
            var email = new Email(emailAddress);
            var mapper = new EmailMapper();
            var parameter = new SqlParameter();

            // Act
            mapper.SetValue(parameter, email);

            // Assert
            parameter.Value.Should().Be(emailAddress);
        }

        [TestMethod]
        public void SetValue_Null_SetNullToParameter()
        {
            // Arrange
            var mapper = new EmailMapper();
            var parameter = new SqlParameter();

            // Act
            mapper.SetValue(parameter, null);

            // Assert
            parameter.Value.Should().Be(DBNull.Value);
        }
    }
}
