using Serilog;
using System;
using WinformsTools.MVVM.Telemetry;

namespace UserManager.Startup
{
    public static class TelemetryConfig
    {
        public static void Config()
        {
            var logger = Serilog.Log.ForContext<Metrics>()
                .ForContext("UserName", GetUserName());

            Metrics.Configure(metric => LogMetric(metric, logger));
        }

        private static void LogMetric(IMetric metric, ILogger logger)
        {
            foreach (var context in metric.Context)
            {
                logger = logger.ForContext(context.Key, context.Value);
            }

#pragma warning disable Serilog004 // Constant MessageTemplate verifier
            logger.Information(metric.MessageTemplate, metric.MessageParams);
#pragma warning restore Serilog004 // Constant MessageTemplate verifier
        }

        private static string GetUserName()
        {
            return $"{Environment.UserDomainName}\\{Environment.UserName}";
        }
    }
}
