using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;
using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.Tests.Model
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void Constructor_InvalidEmail_ThrowValidationException()
        {
            // Arrange
            var invalidEmailAddress = "invalid_email";

            // Act
            Action action = () => new Email(invalidEmailAddress);

            // Assert
            action.Should().Throw<ValidationException>();
        }

        [DataTestMethod]
        [DataRow("valid_email@mail.com")]
        [DataRow("valid.email@mail.com")]
        [DataRow("valid_email@mail.info")]
        [DataRow("valid_email@subdomain.mail.info")]
        public void Constructor_ValidEmail_ReturnCreatedEmail(string validEmailAddress)
        {
            // Act
            var email = new Email(validEmailAddress);

            // Assert
            email.ToString().Should().Be(validEmailAddress);
        }
    }
}
