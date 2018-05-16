using System.Threading.Tasks;
using WeatherForecast.Models;

namespace WeatherForecast.Provider
{
    public interface IWeatherForecastProvider
    {
        /// <summary>
        /// Base uri of external weather forecast provider
        /// </summary>
        string ServiceUri { get; }

        /// <summary>
        /// Name of external weather forecast provider
        /// </summary>
        string ServiceName { get; }

        /// <summary>
        /// Returns weather forecast.
        /// </summary>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="units">Units system</param>
        /// <param name="language">Language</param>
        /// <returns>Returns <see cref="ProviderWeatherForecastModel"/> object.</returns>
        Task<ProviderWeatherForecastModel> GetWeatherForecastAsync(double latitude, double longitude, 
            UnitsSystem units = UnitsSystem.Imperial, Language language = Language.English);
    }
}
