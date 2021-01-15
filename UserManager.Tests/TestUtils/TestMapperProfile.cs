using AutoMapper;

namespace UserManager.Tests.TestUtils
{
    /// <summary>
    /// <see href="https://docs.automapper.org/en/stable/Configuration-validation.html"/>
    /// </summary>
    public static class TestMapperProfile
    {
        public static void Test<TProfile>() where TProfile : Profile, new()
        {
            var configuration = GetMapperConfiguration<TProfile>();

            configuration.AssertConfigurationIsValid();
        }

        public static IMapper GetMapper<TProfile>() where TProfile : Profile, new()
        {
            var configuration = GetMapperConfiguration<TProfile>();
            return configuration.CreateMapper();
        }

        private static MapperConfiguration GetMapperConfiguration<TProfile>() where TProfile : Profile, new()
        {
            return new MapperConfiguration(cfg => cfg.AddProfile<TProfile>());
        }
    }
}
