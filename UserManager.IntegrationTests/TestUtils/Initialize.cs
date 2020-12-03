using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UserManager.IntegrationTests.TestUtils
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
