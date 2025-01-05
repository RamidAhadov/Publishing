using Prometheus;
using PrometheusPublisher.Configuration;
using PrometheusPublisher.Configuration.ConfigItems;
using PrometheusPublisher.Entities.Metrics;

namespace PrometheusPublisher;

public class PublishPerformance
{
    private readonly HostSettings _hostSettings;
    private readonly CounterRequestInterval _counterRequestInterval;
    private readonly GaugeRequestInterval _gaugeRequestInterval;
    private readonly PrometheusMetrics _prometheusMetrics;
    private readonly Intervals _intervals;
    
    
    public PublishPerformance(ConfigFactory configFactory)
    {
        _hostSettings = new HostSettings(configFactory);
        _counterRequestInterval = new CounterRequestInterval(configFactory);
        _gaugeRequestInterval = new GaugeRequestInterval(configFactory);
        _prometheusMetrics = new PrometheusMetrics(configFactory);
        _intervals = new Intervals(configFactory);
        
        var server = new KestrelMetricServer(_hostSettings.Server, _hostSettings.Port);
        server.Start();

        Console.WriteLine($"Started on {_hostSettings.Server}:{_hostSettings.Port}");
    }

    public void Publish()
    {
        Gauge gauge = Metrics.CreateGauge("received_requests","Received requests");
        
        publishGaugeMetrics(gauge);
    }

    public void PublishConfigMetrics()
    {
        List<MetricAndIntervalPair> pairs = Functions.MergeMetricsAndIntervals(_intervals.MetricIntervals, _prometheusMetrics.Metrics);
        List<Task> tasks = new List<Task>();
        foreach (MetricAndIntervalPair pair in pairs)
        {
            switch (pair.Metric)
            {
                case Gauge gauge:
                    tasks.Add(Task.Run(() => publishGaugeMetrics(gauge, (GaugeRequestInterval)pair.Interval)));
                    break;
                case Counter counter:
                    tasks.Add(Task.Run(() => publishCounterMetrics(counter, (CounterRequestInterval)pair.Interval)));
                    break;
                case Histogram histogram:
                    //TODO: Implement histogram
                    break;
            }
        }
        
        Task.WaitAll(tasks.ToArray());
    }

    private void publishGaugeMetrics(Gauge gauge, GaugeRequestInterval? requestInterval = null)
    {
        var interval = requestInterval ?? _gaugeRequestInterval;
        Random random = new Random();
        int requestCount = 0;

        while (true)
        {
            if (requestCount == interval.RequestLimit)
            {
                Console.WriteLine($"Stopped publishing of {gauge.Name} after {interval.RequestLimit} requests");
                
                break;
            }
            
            gauge.Inc(random.Next(interval.IncInterval[0], interval.IncInterval[1]));
            Thread.Sleep(random.Next(interval.TimeInterval[0], interval.TimeInterval[1]));

            if (gauge.Value > interval.UpperBound)
            {
                gauge.DecTo(gauge.Value - (gauge.Value - interval.UpperBound) - random.Next(interval.DecInterval[0], interval.DecInterval[1]));
            }
            
            requestCount++;
        }
    }
    
    private void publishCounterMetrics(Counter counter, RequestInterval? requestInterval = null)
    {
        var interval = requestInterval ?? _counterRequestInterval;
        Random random = new Random();
        int requestCount = 0;

        while (true)
        {
            if (requestCount == interval.RequestLimit)
            {
                Console.WriteLine($"Stopped publishing of {counter.Name} after {interval.RequestLimit} requests");

                break;
            }
            
            counter.Inc(random.Next(interval.IncInterval[0], interval.IncInterval[1]));
            Thread.Sleep(random.Next(interval.TimeInterval[0], interval.TimeInterval[1]));
            
            requestCount++;
        }
    }
}