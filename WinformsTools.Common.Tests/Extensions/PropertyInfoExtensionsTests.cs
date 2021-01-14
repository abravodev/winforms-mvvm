using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using WinformsTools.Common.Extensions;

namespace WinformsTools.Common.Tests.Extensions
{
    [TestClass]
    public class PropertyInfoExtensionsTests
    {
        private class AnyClass
        {
            [DisplayName]
            public int PropertyWithCustomAttribute { get; set; }

            public int PropertyWithoutCustomAttribute { get; set; }
        }

        [TestMethod]
        public void HasCustomAttribute_PropertyWithAttribute_ReturnsTrue()
        {
            // Arrange
            var property = typeof(AnyClass).GetProperty(nameof(AnyClass.PropertyWithCustomAttribute));

            // Act
            var hasCustomAttribute = property.HasCustomAttribute<DisplayNameAttribute>();

            // Assert
            hasCustomAttribute.Should().BeTrue();
        }

        [TestMethod]
        public void HasCustomAttribute_PropertyWithoutAttribute_ReturnsFalse()
        {
            // Arrange
            var property = typeof(AnyClass).GetProperty(nameof(AnyClass.PropertyWithoutCustomAttribute));

            // Act
            var hasCustomAttribute = property.HasCustomAttribute<DisplayNameAttribute>();

            // Assert
            hasCustomAttribute.Should().BeFalse();
        }
    }
}
