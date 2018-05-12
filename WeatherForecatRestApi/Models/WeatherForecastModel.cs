using System.Collections.Generic;

namespace WeatherForecatRestApi.Models
{
    public class WeatherForecastModel
    {
        public List<ItemWeatherForecastModel> Providers { get; set; }
        public ItemWeatherForecastModel AvarageValues { get; set; }
    }
}
