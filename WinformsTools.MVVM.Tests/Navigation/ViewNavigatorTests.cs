using Easy.MessageHub;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SimpleInjector;
using System;
using WinformsTools.MVVM.Core;
using WinformsTools.MVVM.Navigation;
using WinformsTools.MVVM.Tests.TestUtils.Fakes;

namespace WinformsTools.MVVM.Tests.Navigation
{
    [TestClass]
    public class ViewNavigatorTests
    {
        private Container _container;
        private IMessageHub _eventAggregator;
        private IRegisteredViews _registeredViews;
        private ViewNavigator sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _container = new Container();
            _eventAggregator = Substitute.For<IMessageHub>();
            _registeredViews = Substitute.For<IRegisteredViews>();
            sut = new ViewNavigator(_container, _eventAggregator, _registeredViews);
        }

        [TestMethod]
        public void Get_ARegisteredView_ReturnTheViewCreatedAndSetup()
        {
            // Arrange
            var anyView = new FakeView();
            _container.Register<Func<IView<FakeViewModel>>>(() => () => anyView);

            // Act
            var setupView = sut.Get<FakeViewModel>();

            // Assert
            setupView.Should().Be(anyView);
            _registeredViews.Received().Add(anyView);
        }
    }
}
