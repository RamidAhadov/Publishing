using PrometheusPublisher.Configuration.ConfigItems.Dtos;
using PrometheusPublisher.Tools;

namespace PrometheusPublisher.Configuration.ConfigItems;

public class Intervals : IConfigItem
{
    public Intervals()
    {
        
    }
    public Intervals(ConfigFactory configFactory)
    {
        configFactory.Bind(this);
    }
    
    public List<RequestIntervalDTO> MetricIntervals { get; set; }
}