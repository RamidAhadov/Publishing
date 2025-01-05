namespace PrometheusPublisher.Configuration.ConfigItems;

public class RequestInterval : IConfigItem
{
    public RequestInterval()
    {
        Functions.ValidateIntervals(this);
    }
    
    public RequestInterval(ConfigFactory factory)
    {
        factory.BindByName(this, SectionName);

        Functions.ValidateIntervals(this);
    }
    
    protected virtual string SectionName => "RequestInterval";
    public List<int> TimeInterval { get; set; } = new List<int>();
    public List<int> IncInterval { get; set; } = new List<int>();
    public int RequestLimit { get; set; }
}