using AutoMapper;
using WeatherForecast.Models;
using WeatherForecatRestApi.Models;

namespace WeatherForecatRestApi.Automapper
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            CreateMap<ProviderWeatherForecastModel, ItemWeatherForecastModel>();
            CreateMap<ServiceWeatherForecastModel, WeatherForecastModel>();
        }
    }
}
