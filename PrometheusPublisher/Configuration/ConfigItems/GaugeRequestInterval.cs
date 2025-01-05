namespace PrometheusPublisher.Configuration.ConfigItems;

public class GaugeRequestInterval : RequestInterval
{
    public GaugeRequestInterval(): base()
    {
        Functions.ValidateIntervals(this);
    }
    
    public GaugeRequestInterval(ConfigFactory factory):base(factory)
    {
        Functions.ValidateIntervals(this);
    }
    

    public List<int> DecInterval { get; set; } = new List<int>();
    public int UpperBound { get; set; }
}