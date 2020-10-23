using SimpleInjector;
using System;
using System.Windows.Forms;
using UserManager.BusinessLogic.Common;
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
        }

        private static void Startup()
        {
            ExceptionHandling.Config();
            IoCConfig.Config();
            Seed(IoCConfig.Container);
        }

        private static void Seed(Container container)
        {
            container = null;
            var userSeed = container.GetInstance<UserSeed>();
            AsyncHelpers.RunSync(() => userSeed.Execute());
        }
    }
}
