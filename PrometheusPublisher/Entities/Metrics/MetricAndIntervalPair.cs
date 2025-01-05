using Prometheus;
using PrometheusPublisher.Configuration.ConfigItems;

namespace PrometheusPublisher.Entities.Metrics;

public class MetricAndIntervalPair : IEntity
{
    public Collector Metric { get; set; }
    public RequestInterval? Interval { get; set; }
}