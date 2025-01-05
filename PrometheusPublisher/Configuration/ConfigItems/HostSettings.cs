namespace PrometheusPublisher.Configuration.ConfigItems;

public class HostSettings
{
    public HostSettings()
    {
        
    }
    
    public HostSettings(ConfigFactory configFactory)
    {
        configFactory.Bind(this);
    }
    
    public string Server { get; set; }
    public int Port { get; set; }
}