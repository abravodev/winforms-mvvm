using SimpleInjector;
using UserManager.BusinessLogic.DataAccess;
using WinformsTools.MVVM.Components;
using UserManager.ViewModels;
using UserManager.Views;
using System.Configuration;
using UserManager.Providers;
using WinformsTools.MVVM.DependencyInjection;
using Easy.MessageHub;
using UserManager.BusinessLogic.DataAccess.Repositories;
using WinformsTools.MVVM.Navigation;
using WinformsTools.MVVM.Controls.SnackbarControl;

namespace UserManager.Startup
{
    public static class IoCConfig
    {
        public static Container Container { get; private set; }

        public static Container Config()
        {
            var container = new Container();
            container.Options.EnableAutoVerification = false;
            container.Options.ConstructorResolutionBehavior = new GreediestConstructorBehavior();

            RegisterMessageHub(container);
            RegisterMappers(container);
            RegisterComponents(container);
            RegisterDataAccess(container);
            RegisterProviders(container);
            RegisterViews(container);

            Container = container;
            return container;
        }

        private static void RegisterMessageHub(Container container)
        {
            container.RegisterSingleton<IMessageHub, MessageHub>();
        }

        private static void RegisterMappers(Container container) => AutomapperConfig.Config(container);

        private static void RegisterComponents(Container container)
        {
            container.RegisterAsInterfaces<MessageDialog>();
        }

        private static void RegisterDataAccess(Container container)
        {
            var databaseConfig = new DatabaseContext(
                conectionString: ConfigurationManager.ConnectionStrings["UsersDatabase"].ConnectionString
            );
            container.RegisterInstance(databaseConfig);
            container.RegisterAsInterfaces<DatabaseService>();
            container.RegisterAsInterfaces<UserRepository>();
            container.RegisterAsInterfaces<RoleRepository>();
        }

        private static void RegisterProviders(Container container)
        {
            container.RegisterAsInterfaces<SettingProvider>();
            container.RegisterAsInterfaces<ViewNavigator>();
            container.RegisterSingleton<IRegisteredViews>(() => new RegisteredViews());
            container.RegisterAsInterfaces<SnackbarMessageProvider>();
        }

        private static void RegisterViews(Container container)
        {
            container.RegisterView<UsersView, UsersViewModel>();
            container.RegisterView<MainView, MainViewModel>();
            container.RegisterView<CreateUserView, CreateUserViewModel>();
            container.RegisterView<RolesView, RolesViewModel>();
        }
    }
}
