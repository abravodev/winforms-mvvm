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

        [TestMethod]
        public void Constructor_ValidEmail_ReturnCreatedEmail()
        {
            // Arrange
            var validEmailAddress = "valid_email@mail.com";

            // Act
            var email = new Email(validEmailAddress);

            // Assert
            email.ToString().Should().Be(validEmailAddress);
        }
    }
}
