using System.Globalization;
using System.Threading;

namespace WinformsTools.Common
{
    public static class SystemContext
    {
        public static void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        public static void SetCulture(string cultureName) => SetCulture(new CultureInfo(cultureName));
    }
}
