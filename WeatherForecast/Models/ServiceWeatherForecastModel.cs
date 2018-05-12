using System.Collections.Generic;

namespace WeatherForecast.Models
{
    public class ServiceWeatherForecastModel
    {
        public List<ProviderWeatherForecastModel> Providers { get; set; }
        public ProviderWeatherForecastModel AvarageValues { get; set; }
    }
}
