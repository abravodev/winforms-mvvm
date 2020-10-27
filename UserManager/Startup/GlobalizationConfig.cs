using System.Globalization;
using System.Threading;
using UserManager.Providers;

namespace UserManager.Startup
{
    public static class GlobalizationConfig
    {
        public static void Config()
        {
            ConfigureCulture(SettingProvider.Instance.GetCurrentCulture());
        }

        private static void ConfigureCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(culture.Name);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture.Name);
        }
    }
}
