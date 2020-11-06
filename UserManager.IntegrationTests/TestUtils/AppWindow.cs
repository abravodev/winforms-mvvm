using FlaUI.Core.AutomationElements;
using FlaUI.UIA2;
using System;
using System.Diagnostics;
using System.Linq;

namespace UserManager.IntegrationTests.TestUtils
{
    public class AppWindow : IDisposable
    {
        private const string ApplicationName = "UserManager";
        private readonly UIA2Automation _automation;
        private readonly FlaUI.Core.Application _app;

        public AppWindow(string applicationPath)
        {
            _app = FlaUI.Core.Application.Launch(applicationPath);
            _automation = new UIA2Automation();
        }

        public Window GetMainWindow() => _app.GetMainWindow(_automation);

        public Window GetWindow(string title) => _app.GetAllTopLevelWindows(_automation).Single(x => x.Title == title);

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
