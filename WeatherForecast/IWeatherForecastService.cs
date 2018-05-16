using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Models;

namespace WeatherForecast
{
    public interface IWeatherForecastService
    {
        /// <summary>
        /// Returns a list of available weather forecast providers.
        /// </summary>
        List<string> Providers { get; }

        /// <summary>
        /// Returns aggregated weather forecast for all available providers.
        /// In addition, it contains avarage values among all providers.
        /// </summary>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="units">Units system</param>
        /// <param name="language">Language</param>
        /// <returns>Returns <see cref="ServiceWeatherForecastModel"/> object.</returns>
        Task<ServiceWeatherForecastModel> GetWeatherForecastAsync(double latitude, double longitude,
         UnitsSystem units = UnitsSystem.Imperial, Language language = Language.English);
    }
}
