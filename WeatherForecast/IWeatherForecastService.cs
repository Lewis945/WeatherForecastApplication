using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Models;

namespace WeatherForecast
{
    public interface IWeatherForecastService
    {
        /// <summary>
        /// 
        /// </summary>
        List<string> Providers { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="units"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        Task<ServiceWeatherForecastModel> GetWeatherForecastAsync(double latitude, double longitude,
         UnitsSystem units = UnitsSystem.Imperial, Language language = Language.English);
    }
}
