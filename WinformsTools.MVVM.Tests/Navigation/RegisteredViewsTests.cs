using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using WinformsTools.MVVM.Core;
using WinformsTools.MVVM.Navigation;
using WinformsTools.MVVM.Tests.TestUtils.Fakes;

namespace WinformsTools.MVVM.Tests.Navigation
{
    [TestClass]
    public class RegisteredViewsTests
    {
        private RegisteredViews sut;

        [TestInitialize]
        public void TestInitialize()
        {
            sut = new RegisteredViews();
        }

        [TestMethod]
        public void Add_AViewThatIsNotAControl_ThrowException()
        {
            // Arrange
            var aViewThatIsNotAControl = Substitute.For<IView<FakeViewModel>>();

            // Act
            Action action = () => sut.Add(aViewThatIsNotAControl);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Add_AnyViewThatIsAControl_ViewIsRegistered()
        {
            // Arrange
            var anyView = new FakeView();

            // Act
            sut.Add(anyView);

            // Assert
            sut.Get<FakeViewModel>().Should().Be(anyView);
        }

        [TestMethod]
        public void Get_AViewThatWasDisposed_ThrowException()
        {
            // Arrange
            var anyView = new FakeView();
            sut.Add(anyView);
            sut.Get<FakeViewModel>().Should().Be(anyView);
            anyView.Dispose();

            // Act
            Action action = () => sut.Get<FakeViewModel>();

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void GetControl_AnyView_ReturnAsControl()
        {
            // Arrange
            var anyView = new FakeView();
            sut.Add(anyView);
            sut.Get<FakeViewModel>().Should().Be(anyView);

            // Act
            var returnedControl = sut.GetControl<FakeViewModel>();

            // Assert
            returnedControl.Should().Be(anyView);
        }
    }
}
