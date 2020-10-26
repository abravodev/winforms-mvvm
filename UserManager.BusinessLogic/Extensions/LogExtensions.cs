using Serilog;
using System;

namespace UserManager.BusinessLogic.Extensions
{
    public static class LogExtensions
    {
        public static void Error(this ILogger logger, Exception ex) => logger.Error(ex, "{ExceptionMessage}", ex.Message);
    }
}
