using Serilog;
using System;
using System.Windows.Forms;
using UserManager.BusinessLogic.DataAccess;
using UserManager.Startup;
using UserManager.Views;

namespace UserManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Startup();

            var mainView = IoCConfig.Container.GetInstance<MainView>();
            Application.Run(mainView);
            Log.CloseAndFlush();
        }

        private static void Startup()
        {
            LogConfig.Config();
            ExceptionHandling.Config();
            IoCConfig.Config();
            ConfigureDapper.Config();
            GlobalizationConfig.Config();
        }
    }
}
