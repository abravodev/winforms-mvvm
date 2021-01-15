using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UserManager.Startup;

namespace UserManager.Tests.Startup
{
    [TestClass]
    public class ExecutionParametersTests
    {
        [TestMethod]
        public void Get_ExistingParameter_ReturnParameterValue()
        {
            // Arrange
            var parameters = ExecutionParameters.Create(("flag", "true"));

            // Act
            var value = parameters.Get("flag");

            // Assert
            value.Should().Be("true");
        }

        [TestMethod]
        public void Get_NonExistingParameter_ReturnNull()
        {
            // Arrange
            var parameters = ExecutionParameters.Create(("flag", "true"));

            // Act
            var value = parameters.Get("another_parameter");

            // Assert
            value.Should().BeNull();
        }

        [TestMethod]
        public void ToString_ReturnListOfParameters()
        {
            // Arrange
            var parameters = new ExecutionParameters(new string[] { "one_flag=true", "second_flag=false", "third_flag" });

            // Act
            var result = parameters.ToString();

            // Assert
            result.Should().Be("one_flag=true second_flag=false third_flag");
        }

        [TestMethod]
        public void Parse_ParameterWithMoreThanOneSeparator_ThrowException()
        {
            // Arrange
            var parameterWithMoreThanOneSeparator = "one=flag=true";

            // Act
            Action action = () => ExecutionParameters.Parse(parameterWithMoreThanOneSeparator);

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}
