using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Threading;

namespace WinformsTools.Common.Tests
{
    [TestClass]
    public class SystemContextTests
    {
        private CultureInfo _currentCulture;
        private CultureInfo _currentUICulture;

        [TestInitialize]
        public void TestInitialize()
        {
            _currentCulture = Thread.CurrentThread.CurrentCulture;
            _currentUICulture = Thread.CurrentThread.CurrentUICulture;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Thread.CurrentThread.CurrentCulture = _currentCulture;
            Thread.CurrentThread.CurrentUICulture = _currentUICulture;
        }

        [TestMethod]
        public void SetCulture_CultureInfo_SetCurrentCultureOfCurrentThread()
        {
            // Arrange
            var cultureInfo = new CultureInfo("en-US");

            // Act
            SystemContext.SetCulture(cultureInfo);

            // Assert
            Thread.CurrentThread.CurrentCulture.Should().BeEquivalentTo(cultureInfo);
            Thread.CurrentThread.CurrentUICulture.Should().BeEquivalentTo(cultureInfo);
        }

        [TestMethod]
        public void SetCulture_CultureInfoName_SetCurrentCultureOfCurrentThread()
        {
            // Arrange
            var cultureInfo = "en-US";

            // Act
            SystemContext.SetCulture(cultureInfo);

            // Assert
            Thread.CurrentThread.CurrentCulture.Name.Should().BeEquivalentTo(cultureInfo);
            Thread.CurrentThread.CurrentUICulture.Name.Should().BeEquivalentTo(cultureInfo);
        }
    }
}
