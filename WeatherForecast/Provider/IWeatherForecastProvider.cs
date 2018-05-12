using System.Threading.Tasks;
using WeatherForecast.Models;

namespace WeatherForecast.Provider
{
    public interface IWeatherForecastProvider
    {
        /// <summary>
        /// 
        /// </summary>
        string ServiceUri { get; }

        /// <summary>
        /// 
        /// </summary>
        string ServiceName { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="temperatureScale"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        Task<ProviderWeatherForecastModel> GetWeatherForecastAsync(double latitude, double longitude, 
            UnitsSystem temperatureScale = UnitsSystem.Imperial, Language language = Language.English);
    }
}
