using System.Collections.Generic;
using System.Globalization;

namespace UserManager.Providers
{
    public interface ISettingProvider
    {
        CultureInfo GetCurrentCulture();

        void SetCurrentCulture(CultureInfo cultureInfo);

        /// <summary>
        /// Return list of available cultures supported by the application
        /// </summary>
        /// <returns></returns>
        List<CultureInfo> GetAvailableCultures();
    }
}