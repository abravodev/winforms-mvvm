using UserManager.Startup;
using WinformsTools.Common;
using WinformsTools.IntegrationTestUtils;

namespace UserManager.IntegrationTests.TestUtils
{
    public class AppWindow : BaseAppWindow
    {
        private const string APP_NAME = "UserManager";
        protected override string ApplicationName => APP_NAME;
        private const string CultureName = "en";

        public AppWindow(string applicationPath)
            : base(
                  applicationPath,
                  arguments: ExecutionParameters.Create(("culture", CultureName)).ToString())
        {
            SystemContext.SetCulture(CultureName);
        }

        public static AppWindow Open() => new AppWindow($"{APP_NAME}.exe");

        public static void KillAllInstances() => KillAllInstances(APP_NAME);
    }
}
