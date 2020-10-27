using System.Globalization;

namespace UserManager.Providers
{
    public interface ISettingProvider
    {
        CultureInfo GetCurrentCulture();

        void SetCurrentCulture(CultureInfo cultureInfo);
    }
}