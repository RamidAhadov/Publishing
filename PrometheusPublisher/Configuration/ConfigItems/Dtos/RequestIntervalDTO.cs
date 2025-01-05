namespace PrometheusPublisher.Configuration.ConfigItems.Dtos;

public class RequestIntervalDTO
{
    public RequestIntervalDTO()
    {
        Functions.ValidateIntervals(this);
    }

    public RequestIntervalDTO(ConfigFactory configFactory)
    {
        configFactory.Bind(this);
        Functions.ValidateIntervals(this);
    }
    
    public int MetricId { get; set; }
    public string MetricType { get; set; }
    public List<int> TimeInterval { get; set; } = new List<int>();
    public List<int> IncInterval { get; set; } = new List<int>();
    public int RequestLimit { get; set; }
    public List<int> DecInterval { get; set; } = new List<int>();
    public int UpperBound { get; set; }
}