using App.Metrics;
using App.Metrics.Counter;

namespace Callisto.Core.Metrics
{
    /// <summary>
    /// Defines the <see cref="MetricsRegistry" />
    /// </summary>
    public static class MetricsRegistry
    {
        /// <summary>
        /// Gets the NotificiationCounter
        /// </summary>
        public static CounterOptions NotificiationCounter => new CounterOptions
        {
            Name = "Notifications Sent",
            MeasurementUnit = Unit.Events
        };
    }
}
