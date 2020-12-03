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
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<TProfile>());

            configuration.AssertConfigurationIsValid();
        }
    }
}
