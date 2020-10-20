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

            Application.Run(new Form1());
        }

        private static void Startup()
        {
            IoCConfig.Config();
            Seed();
        }

        private static void Seed()
        {
            var userSeed = IoCConfig.Container.GetInstance<UserSeed>();
            AsyncHelpers.RunSync(() => userSeed.Execute());
        }
    }
}
