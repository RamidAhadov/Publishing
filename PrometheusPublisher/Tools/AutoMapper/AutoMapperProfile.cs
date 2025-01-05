using AutoMapper;
using PrometheusPublisher.Configuration.ConfigItems;
using PrometheusPublisher.Configuration.ConfigItems.Dtos;

namespace PrometheusPublisher.Tools;

public class AutoMapperProfile:Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RequestIntervalDTO, CounterRequestInterval>();
        CreateMap<RequestIntervalDTO, GaugeRequestInterval>();
    }
}