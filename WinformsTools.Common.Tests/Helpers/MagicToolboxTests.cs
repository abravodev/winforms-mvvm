using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WinformsTools.Common.Helpers;

namespace WinformsTools.Common.Tests.Helpers
{
    [TestClass]
    public class MagicToolboxTests
    {
        public class AnyClass
        {
            public event EventHandler Event;
        }

        [TestMethod]
        public void GetEvent_EventWithHandlers_InvokeEventHandlerByReflection()
        {
            // Arrange
            var anyClass = new AnyClass();
            var receivedSender = default(object);
            var receivedEventArgs = default(EventArgs);
            var anyEventArgs = EventArgs.Empty;
            anyClass.Event += (sender, e) => { receivedSender = sender; receivedEventArgs = e; };

            // Act
            var evt = MagicToolbox.GetEvent(anyClass, nameof(AnyClass.Event));
            evt.Invoke(this, anyEventArgs);

            // Assert
            receivedSender.Should().BeEquivalentTo(this);
            receivedEventArgs.Should().BeEquivalentTo(anyEventArgs);
        }

        [TestMethod]
        public void GetEvent_EventWithoutHandlers_ReturnNull()
        {
            // Arrange
            var anyClass = new AnyClass();

            // Act
            var evt = MagicToolbox.GetEvent(anyClass, nameof(AnyClass.Event));

            // Assert
            evt.Should().BeNull();
        }
    }
}
