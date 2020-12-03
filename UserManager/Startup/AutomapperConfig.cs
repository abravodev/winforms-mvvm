using AutoMapper;
using SimpleInjector;
using UserManager.Mappers;

namespace UserManager.Startup
{
    public static class AutomapperConfig
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<RoleProfile>();
            });
        }

        public static void Config(Container container)
        {
            var config = GetMapperConfiguration();
            container.Register<IMapper>(() => config.CreateMapper(container.GetInstance));
        }
    }
}
