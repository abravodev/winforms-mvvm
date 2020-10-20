using SimpleInjector;
using SimpleInjector.Diagnostics;
using UserManager.BusinessLogic.DataAccess;
using UserManager.ViewModels;
using UserManager.Views;

namespace UserManager.Startup
{
    public static class IoCConfig
    {
        public static Container Container { get; private set; }

        public static void Config()
        {
            var container = new Container();

            RegisterDataAccess(container);
            RegisterViews(container);

            Container = container;
        }
        
        private static void RegisterDataAccess(Container container)
        {
            container.Register<IUserRepository, UserRepository>(Lifestyle.Singleton);
            container.Register(() => new UserSeed(container.GetInstance<IUserRepository>()));
        }

        private static void RegisterViews(Container container)
        {
            container.RegisterView<UsersView, UsersViewModel>();
        }
    }
}
