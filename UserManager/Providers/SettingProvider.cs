using System.Collections.Generic;
using System.Globalization;
using UserManager.Properties;

namespace UserManager.Providers
{
    public class SettingProvider : ISettingProvider
    {
        public static SettingProvider Instance { get; } = new SettingProvider();

        public List<CultureInfo> GetAvailableCultures()
        {
            return new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("es") };
        }

        public CultureInfo GetCurrentCulture()
        {
            return new CultureInfo(Settings.Default.UserCulture);
        }

        public void SetCurrentCulture(CultureInfo cultureInfo)
        {
            Settings.Default.UserCulture = cultureInfo.Name;
            Settings.Default.Save();
        }
    }
}
