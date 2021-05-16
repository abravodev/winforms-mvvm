using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WinformsTools.MVVM.Bindings;

namespace WinformsTools.MVVM.Tests.Bindings
{
    [TestClass]
    public class ReflectionUtilsTests
    {
        private class Dummy
        {
            public int Age { get; set; }

            public DateTime BirthDate { get; set; }
        }

        [TestMethod]
        public void GetPropertyName()
        {
            // Act
            var propertyName = ReflectionUtils.GetPropertyName<Dummy, int>(e => e.Age);

            // Assert
            propertyName.Should().Be(nameof(Dummy.Age));
        }

        [TestMethod]
        public void GetValue()
        {
            // Arrange
            var dummy = new Dummy { Age = 1 };

            // Act
            var propertyValue = ReflectionUtils.GetValue(dummy, e => e.Age);

            // Assert
            propertyValue.Should().Be(dummy.Age);
        }

        [TestMethod]
        public void SetValue()
        {
            // Arrange
            var dummy = new Dummy { Age = 1 };
            var newAge = 2;

            // Act
            ReflectionUtils.SetValue(dummy, e => e.Age, newAge);

            // Assert
            dummy.Age.Should().Be(newAge);
        }

        [TestMethod]
        public void GetFullPath_BirthDateYear()
        {
            // Act
            var fullPath = ReflectionUtils.GetFullPath<Dummy, int>(x => x.BirthDate.Year);

            // Assert
            fullPath.Should().Be("BirthDate.Year");
        }

        [TestMethod]
        public void GetFullPath_Age()
        {
            // Act
            var fullPath = ReflectionUtils.GetFullPath<Dummy, int>(x => x.Age);

            // Assert
            fullPath.Should().Be("Age");
        }
    }
}
