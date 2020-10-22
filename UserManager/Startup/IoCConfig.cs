using AutoMapper;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UserManager.BusinessLogic.DataAccess;
using UserManager.Components;
using UserManager.Mappers;
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

            RegisterMappers(container);
            RegisterComponents(container);
            RegisterDataAccess(container);
            RegisterViews(container);

            Container = container;
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
        }
    }
}
