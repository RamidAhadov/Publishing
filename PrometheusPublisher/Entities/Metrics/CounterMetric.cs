using PrometheusPublisher.Configuration.ConfigItems;

namespace PrometheusPublisher.Entities.Metrics;

public class CounterMetric : Metric
{
    public CounterRequestInterval RequestInterval { get; set; }
}