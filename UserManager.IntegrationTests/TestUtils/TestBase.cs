using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UserManager.IntegrationTests.TestUtils
{
    public class TestBase
    {
        protected AppWindow App { get; private set; }

        [TestInitialize]
        public void TestInitialize()
        {
            App = AppWindow.Open();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            App.Close();
            App.Dispose();
        }
    }
}
