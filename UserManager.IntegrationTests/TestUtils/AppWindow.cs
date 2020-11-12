using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using System;
using System.Diagnostics;
using System.Linq;

namespace UserManager.IntegrationTests.TestUtils
{
    public class AppWindow : IDisposable
    {
        private const string ApplicationName = "UserManager";
        private readonly AutomationBase _automation;
        private readonly Application _app;

        public AppWindow(string applicationPath)
        {
            _app = Application.Launch(applicationPath);
            _automation = new FlaUI.UIA2.UIA2Automation();
        }

        public Window GetMainWindow() => _app.GetMainWindow(_automation);

        public Window GetWindow(string title)
        {
            return Retry.WhileNull(() => GetWindows().SingleOrDefault(x => x.Title == title)).Result;
        }

        public Window[] GetWindows() => _app.GetAllTopLevelWindows(_automation);

        public Window GetUsersWindow() => GetWindow("UsersView");

        public void Close() => _app.Close();

        public void Dispose() => _automation.Dispose();

        public static AppWindow Open() => new AppWindow($"{ApplicationName}.exe");

        public static void KillAllInstances()
        {
            foreach (var process in Process.GetProcessesByName(ApplicationName))
            {
                process.Kill();
            }
        }
    }
}
