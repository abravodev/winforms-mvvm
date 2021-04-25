using System;
using System.Collections.Generic;

namespace WinformsTools.MVVM.Telemetry
{
    public interface IMetric
    {
        string MessageTemplate { get; }

        object[] MessageParams { get; }

        IDictionary<string, object> Context { get; }
    }
}
