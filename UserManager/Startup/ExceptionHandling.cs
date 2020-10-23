using System;
using System.Threading;
using System.Windows.Forms;

namespace UserManager.Startup
{
    public static class ExceptionHandling
    {
        /// <summary>
        /// <see href="https://www.csharp-examples.net/catching-unhandled-exceptions/"/>
        /// </summary>
        /// <param name="application"></param>
        public static void Config()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled UI Exception");
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Unhandled Thread Exception");
        }
    }
}
