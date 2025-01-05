using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace PrometheusPublisher.Tools;

public sealed class Services
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
            _instance = new Services();
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