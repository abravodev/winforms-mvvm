using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.Tests.Model
{

    [TestClass]
    public class EmailAttributeTests
    {
        private class UserWithMandatoryEmail
        {
            [Email]
            [Required]
            public string Email { get; set; }
        }

        private class UserWithOptionalEmail
        {
            [Email]
            public string Email { get; set; }
        }

        private class UserWithWrongType
        {
            [Email]
            public int Email { get; set; }
        }

        [TestMethod]
        public void IsValid_UserWithWrongType_ThrowArgumentException()
        {
            // Arrange
            var user = new UserWithWrongType { Email = 0 };

            // Act
            Action action = () => ValidateModel(user);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataTestMethod]
        [DataRow(null, false)]
        [DataRow("", false)]
        [DataRow("valid_email@mail.com", true)]
        public void IsValid_UserNeedsEmail_ReturnsTrueIfAValidOne(string emailAddress, bool expectedValidity)
        {
            // Arrange
            var user = new UserWithMandatoryEmail { Email = emailAddress };

            // Act
            var (isValid, _) = ValidateModel(user);

            // Assert
            isValid.Should().Be(expectedValidity);
        }

        [DataTestMethod]
        [DataRow(null, true)]
        [DataRow("", false)]
        [DataRow("valid_email@mail.com", true)]
        public void IsValid_UserDoesNotNeedEmail_ReturnsTrueIfAValidOneOrNotProvided(string emailAddress, bool expectedValidity)
        {
            // Arrange
            var user = new UserWithOptionalEmail { Email = emailAddress };

            // Act
            var (isValid, _) = ValidateModel(user);

            // Assert
            isValid.Should().Be(expectedValidity);
        }

        private (bool IsValid, List<ValidationResult> Results) ValidateModel(object model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results, true);
            return (isValid, results);
        }
    }
}
