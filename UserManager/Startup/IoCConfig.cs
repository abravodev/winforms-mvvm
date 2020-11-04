using AutoMapper;
using SimpleInjector;
using UserManager.BusinessLogic.DataAccess;
using MvvmTools.Components;
using UserManager.Mappers;
using UserManager.ViewModels;
using UserManager.Views;
using System.Configuration;
using UserManager.Providers;
using MvvmTools.DependencyInjection;

namespace UserManager.Startup
{
    public static class IoCConfig
    {
        public static Container Container { get; private set; }

        public static Container Config()
        {
            var container = new Container();
            container.Options.ConstructorResolutionBehavior =
                new GreediestConstructorBehavior();

            RegisterMappers(container);
            RegisterComponents(container);
            RegisterDataAccess(container);
            RegisterProviders(container);
            RegisterViews(container);

            Container = container;
            return container;
        }

        private static void RegisterMappers(Container container)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });

            container.Register<IMapper>(() => config.CreateMapper(container.GetInstance));
        }

        private static void RegisterComponents(Container container)
        {
            container.Register<IMessageDialog, MessageDialog>();
        }

        private static void RegisterDataAccess(Container container)
        {
            var databaseConfig = new DatabaseContext(
                conectionString: ConfigurationManager.ConnectionStrings["UsersDatabase"].ConnectionString
            );
            container.RegisterInstance(databaseConfig);
            container.Register<IUserRepository, UserRepository>();
        }

        private static void RegisterProviders(Container container)
        {
            container.Register<ISettingProvider, SettingProvider>();
            container.Register<IViewNavigator, ViewNavigator>();
        }

        private static void RegisterViews(Container container)
        {
            container.RegisterView<UsersView, UsersViewModel>();
            container.RegisterView<MainView, MainViewModel>();
            container.RegisterView<CreateUserView, CreateUserViewModel>();
        }
    }
}
