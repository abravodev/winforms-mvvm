using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinformsTools.MVVM.Bindings;

namespace WinformsTools.MVVM.Tests.Bindings
{
    [TestClass]
    public class ReflectionUtilsTests
    {
        private class Dummy
        {
            public int Age { get; set; }
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
    }
}
