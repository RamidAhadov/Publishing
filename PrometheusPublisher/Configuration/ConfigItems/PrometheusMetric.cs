using Prometheus;

namespace PrometheusPublisher.Configuration.ConfigItems;

public class PrometheusMetric : IConfigItem
{
    public int Id { get; set; }
    public string MetricType { get; set; }
    public string MetricName { get; set; }
    public string Help { get; set; }
    public string[] Labels { get; set; }
    public ICollector Collector { get; set; }
    public bool NeedsRequestInterval { get; set; }
}