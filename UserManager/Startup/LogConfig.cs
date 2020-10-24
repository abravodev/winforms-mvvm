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
                .WriteTo.File(
                    path: ConfigurationManager.AppSettings["LogPath"], 
                    rollingInterval: RollingInterval.Day, 
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}\t{Properties:j}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
