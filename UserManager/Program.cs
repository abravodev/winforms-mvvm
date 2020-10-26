using Serilog;
using System;
using System.Windows.Forms;
using UserManager.BusinessLogic.DataAccess;
using UserManager.Startup;

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

            var mainForm = IoCConfig.Container.GetInstance<Form1>();
            Application.Run(mainForm);
            Log.CloseAndFlush();
        }

        private static void Startup()
        {
            LogConfig.Config();
            ExceptionHandling.Config();
            IoCConfig.Config();
            ConfigureDapper.Config();
        }
    }
}
