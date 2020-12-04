using WinformsTools.IntegrationTestUtils;

namespace UserManager.IntegrationTests.TestUtils
{
    public class AppWindow : BaseAppWindow
    {
        private const string APP_NAME = "UserManager";
        protected override string ApplicationName => APP_NAME;

        public AppWindow(string applicationPath) : base(applicationPath) { }

        public static AppWindow Open() => new AppWindow($"{APP_NAME}.exe");

        public static void KillAllInstances() => KillAllInstances(APP_NAME);
    }
}
