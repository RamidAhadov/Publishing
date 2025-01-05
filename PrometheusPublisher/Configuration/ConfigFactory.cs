using System.Reflection;
using Microsoft.Extensions.Configuration;
using PrometheusPublisher.Configuration.ConfigItems;
using PrometheusPublisher.Configuration.Exceptions;

namespace PrometheusPublisher.Configuration;

public class ConfigFactory
{
    private readonly IConfigurationRoot _configuration;

    public ConfigFactory()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("/etc/PublisherApp/Configuration/appsettings.json", optional: true, reloadOnChange: true);
        
        _configuration = builder.Build();
    }
    
    public IConfigItem GetConfiguration(string section)
    {
        var configSection = _configuration.GetSection(section);
        if (configSection.Exists())
        {
            var configType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.Name.Equals(section, StringComparison.OrdinalIgnoreCase) && typeof(IConfigItem).IsAssignableFrom(t));
            if (configType != null)
            {
                var configInstance = (IConfigItem)configSection.Get(configType);
                return configInstance;
            }
        }

        throw new ConfigSectionException(section);
    }
    
    public void Bind(object section)
    {
        string sectionName = section.GetType().Name;
        
        bind(section, sectionName);
    }

    public void BindByName(object section, string sectionName)
    {
        bind(section, sectionName);
    }

    private void bind(object section, string sectionName)
    {
        var configSection = _configuration.GetSection(sectionName);
        if (configSection.Exists())
        {
            configSection.Bind(section);
        }
    }
}