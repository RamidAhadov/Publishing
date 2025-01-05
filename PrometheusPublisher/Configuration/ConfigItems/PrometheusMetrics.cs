using Prometheus;
using PrometheusPublisher.Configuration.Exceptions;

namespace PrometheusPublisher.Configuration.ConfigItems;

public class PrometheusMetrics : IConfigItem
{
    public PrometheusMetrics()
    {
        
    }

    public PrometheusMetrics(ConfigFactory configFactory)
    {
        configFactory.Bind(this);
    }

    public List<PrometheusMetric> Metrics { get; set; }
}