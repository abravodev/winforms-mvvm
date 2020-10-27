using System.Globalization;
using UserManager.Properties;

namespace UserManager.Providers
{
    public class SettingProvider : ISettingProvider
    {
        public static SettingProvider Instance { get; } = new SettingProvider();

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
