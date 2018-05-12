using System.Threading.Tasks;
using WeatherForecast.Models;

namespace WeatherForecastTestApplication.WeatherForecast
{
    public interface IWeatherForecastProvider
    {
        Task<TemperatureForecastModel> GetTemperatureAsync(double latitude, double longitude, 
            TemperatureScale temperatureScale = TemperatureScale.Kelvin, Language language = Language.English);
    }
}
