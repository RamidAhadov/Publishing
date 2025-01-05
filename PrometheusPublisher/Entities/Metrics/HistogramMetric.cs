using Prometheus;

namespace PrometheusPublisher.Entities.Metrics;

public class HistogramMetric : Metric
{
    public ICollector? Collector { get; set; }
}