using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserManager.IntegrationTests.TestUtils;

namespace UserManager.IntegrationTests
{
    [TestClass]
    public class Initialize
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context) => AppWindow.KillAllInstances();

        [AssemblyCleanup]
        public static void AssemblyCleanup() => AppWindow.KillAllInstances();
    }
}
