using System.Reflection;
using Prometheus;
using PrometheusPublisher.Configuration.ConfigItems;
using PrometheusPublisher.Configuration.ConfigItems.Dtos;
using PrometheusPublisher.Entities.Metrics;
using PrometheusPublisher.Tools;

namespace PrometheusPublisher;

public static class Functions
{
    public static void ValidateIntervals(object obj)
    {
        var properties = obj.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.PropertyType == typeof(List<int>) &&
                        p.Name.Contains("Interval", StringComparison.OrdinalIgnoreCase) &&
                        p.GetValue(obj) is List<int> list &&
                        list.Count == 2);
        
        foreach (var property in properties)
        {
            var list = (List<int>)property.GetValue(obj);
            if (list != null && list[0] > list[1])
            {
                (list[0], list[1]) = (list[1], list[0]);
            }
        }
    }

    public static List<MetricAndIntervalPair> MergeMetricsAndIntervals(
        List<RequestIntervalDTO> requestIntervalDtos,
        List<PrometheusMetric> metrics)
    {
        var list = new List<MetricAndIntervalPair>();
        foreach (PrometheusMetric metric in metrics)
        {
            var pair = new MetricAndIntervalPair
            {
                Metric = createCollector(metric)
            };
                
            if (metric.NeedsRequestInterval)
            {
                pair.Interval = createRequestInterval(requestIntervalDtos.First(i => i.MetricId == metric.Id));
            }
            
            list.Add(pair);
        }
        
        return list;
    }

    private static Collector createCollector(PrometheusMetric metric)
    {
        switch (metric.MetricType)
        {
            case "Gauge":
                return Metrics.CreateGauge(name: metric.MetricName, help: metric.Help,
                    labelNames: metric.Labels);
            case "Counter":
                return Metrics.CreateCounter(name: metric.MetricName, help: metric.Help,
                    labelNames: metric.Labels);
            case "Histogram":
                return Metrics.CreateHistogram(name: metric.MetricName, help: metric.Help,
                    labelNames: metric.Labels);
            default:
                throw new Exception($"Wrong metric type: {metric.MetricType}");
        }
    }

    private static RequestInterval? createRequestInterval(RequestIntervalDTO requestIntervalDto)
    {
        var services = Services.GetInstance();
        var mapper = services.Mapper;
        switch (requestIntervalDto.MetricType)
        {
            case "Counter":
                return mapper.Map<CounterRequestInterval>(requestIntervalDto);
            case "Gauge":
                return mapper.Map<GaugeRequestInterval>(requestIntervalDto);
            case "Histogram":
                return null;
            default:
                return null;
        }
    }
}