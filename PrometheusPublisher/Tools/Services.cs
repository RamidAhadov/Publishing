using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace PrometheusPublisher.Tools;

public class Services
{
    private Services()
    {
        ServiceCollection serviceCollection = new ServiceCollection();
        
        registerMapper(serviceCollection);
    }

    public static Services GetInstance()
    {
        if (_instance == null)
        {
            return new Services();
        }
        
        return _instance;
    }
    
    private static Services _instance;

    private void registerMapper(ServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile));

        var provider = services.BuildServiceProvider();

        Mapper = provider.GetRequiredService<IMapper>();
    }

    public IMapper Mapper { get; set; }
}