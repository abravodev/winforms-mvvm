using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using WinformsTools.MVVM.Core;
using WinformsTools.MVVM.Navigation;
using WinformsTools.MVVM.Tests.TestUtils.Fakes;

namespace WinformsTools.MVVM.Tests.Navigation
{
    [TestClass]
    public class ViewClosedEventTests
    {
        [TestMethod]
        public void Create_AnyView_ReturnEventOfClosedEvent()
        {
            // Arrange
            var anyView = new FakeView();
            var expectedCloseEvent = new ViewClosedEvent("FakeView", "FakeViewModel");

            // Act
            var closedEvent = ViewClosedEvent.Create(anyView);

            // Assert
            closedEvent.Should().BeEquivalentTo(expectedCloseEvent);
        }
    }
}
