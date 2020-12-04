using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using System;
using System.Diagnostics;
using System.Linq;

namespace WinformsTools.IntegrationTestUtils
{
    public abstract class BaseAppWindow : IDisposable
    {
        protected abstract string ApplicationName { get; }
        protected readonly AutomationBase _automation;
        protected readonly Application _app;

        protected BaseAppWindow(string applicationPath, string arguments = null)
        {
            _app = Application.Launch(applicationPath, arguments);
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

        public static void KillAllInstances(string appName)
        {
            foreach (var process in Process.GetProcessesByName(appName))
            {
                process.Kill();
            }
        }
    }
}
