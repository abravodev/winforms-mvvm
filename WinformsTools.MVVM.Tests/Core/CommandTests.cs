using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using WinformsTools.MVVM.Core;

namespace WinformsTools.MVVM.Tests.Core
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public async Task From_ActionWithParameter_ReturnActionWrappedInTask()
        {
            // Arrange
            var setVariable = 0;
            Action<int> anyAction = number => setVariable = number;
            var parameterValue = 1;

            // Act
            var command = Command.From(anyAction);

            // Assert
            command.Should().BeOfType<Command<int>>();
            await command.Execute(parameterValue);
            setVariable.Should().Be(parameterValue);

        }
    }
}
