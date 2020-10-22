using AutoMapper;
using SimpleInjector;
using UserManager.BusinessLogic.DataAccess;
using MvvmTools.Components;
using UserManager.Mappers;
using UserManager.ViewModels;
using UserManager.Views;
using MvvmTools.Common;

namespace UserManager.Startup
{
    public static class IoCConfig
    {
        public static Container Container { get; private set; }

        public static Container Config()
        {
            var container = new Container();

            RegisterMappers(container);
            RegisterComponents(container);
            RegisterDataAccess(container);
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
            container.Register<IUserRepository, UserRepository>(Lifestyle.Singleton);
            container.Register(() => new UserSeed(container.GetInstance<IUserRepository>()));
        }

        private static void RegisterViews(Container container)
        {
            container.RegisterView<UsersView, UsersViewModel>();
            container.RegisterView<Form1>();
        }
    }
}
