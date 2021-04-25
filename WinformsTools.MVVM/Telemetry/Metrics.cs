using System;

namespace WinformsTools.MVVM.Telemetry
{
    public class Metrics
    {
        private static Action<IMetric> _onLog;

        public static void Configure(Action<IMetric> onLog)
        {
            _onLog = onLog;
        }

        public static void Log(IMetric metric) => _onLog(metric);
    }
}
