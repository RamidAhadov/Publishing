using PrometheusPublisher.Configuration.ConfigItems;

namespace PrometheusPublisher.Entities.Metrics;

public class Metric : IEntity
{
    public int Id { get; set; }
    public string MetricType { get; set; }
    public string MetricName { get; set; }
    public string Help { get; set; }
    public string[] Labels { get; set; }
    public bool NeedsRequestInterval { get; set; }
    public virtual RequestInterval RequestInterval { get; set; }
}