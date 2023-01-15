using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WinformsTools.MVVM.Telemetry;

namespace WinformsTools.MVVM.Tests.Telemetry
{
    [TestClass]
    public class MetricsTests
    {
        [TestMethod]
        public void Log_AnyMetric_MethodConfiguredIsCalled()
        {
            // Arrange
            var metric = new FakeMetric("This is a {Test}", "testMessage");
            IMetric receivedMetric = null;
            Metrics.Configure(x => receivedMetric = x);

            // Act
            Metrics.Log(metric);

            // Assert
            receivedMetric.Should().Be(metric);
        }

        [TestMethod]
        public void Log_NotConfigured_DoNothing()
        {
            // Arrange
            var metric = new FakeMetric("This is a {Test}", "testMessage");

            // Act
            Action action = () => Metrics.Log(metric);

            // Assert
            action.Should().NotThrow();
        }

        private class FakeMetric : IMetric
        {
            public FakeMetric(string messageTemplate, params object[] messageParams)
            {
                MessageTemplate = messageTemplate;
                MessageParams = messageParams;
            }

            public string MessageTemplate { get; }

            public object[] MessageParams { get; }

            public IDictionary<string, object> Context => new Dictionary<string, object>();
        }
    }
}
