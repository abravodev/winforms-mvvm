using System.Globalization;
using UserManager.Providers;
using WinformsTools.Common;

namespace UserManager.Startup
{
    public static class GlobalizationConfig
    {
        public static void Config(string overridingCulture)
        {
            if (!string.IsNullOrEmpty(overridingCulture))
            {
                SettingProvider.Instance.SetCurrentCulture(new CultureInfo(overridingCulture));
            }

            SystemContext.SetCulture(SettingProvider.Instance.GetCurrentCulture());
        }
    }
}
