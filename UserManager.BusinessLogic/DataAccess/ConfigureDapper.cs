using Dapper;
using UserManager.BusinessLogic.DataAccess.Mappers;

namespace UserManager.BusinessLogic.DataAccess
{
    public static class ConfigureDapper
    {
        public static void Config()
        {
            ConfigureMappers();
        }

        private static void ConfigureMappers()
        {
            SqlMapper.AddTypeHandler(new EmailMapper());
            SqlMapper.AddTypeHandler(new RoleMapper());
        }
    }
}
