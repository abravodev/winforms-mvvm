using Serilog;
using System.Configuration;

namespace UserManager.Startup
{
    public static class LogConfig
    {
        public static void Config()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(ConfigurationManager.AppSettings["LogPath"], rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
