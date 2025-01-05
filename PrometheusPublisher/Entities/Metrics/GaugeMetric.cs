using PrometheusPublisher.Configuration;
using PrometheusPublisher.Configuration.ConfigItems;

namespace PrometheusPublisher.Entities.Metrics;

public class GaugeMetric : Metric
{
    public GaugeRequestInterval RequestInterval { get; set; }
}