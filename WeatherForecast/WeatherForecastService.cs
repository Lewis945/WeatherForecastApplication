using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Exceptions;
using WeatherForecast.Logging;
using WeatherForecast.Models;
using WeatherForecast.Provider;

namespace WeatherForecast
{
    public class WeatherForecastService : IWeatherForecastService
    {
        #region Properties

        /// <inheritdoc />
        public List<string> Providers => _providers?.Select(p => p.ServiceName).ToList();

        #endregion

        #region Fields

        private readonly IEnumerable<IWeatherForecastProvider> _providers;
        private readonly ILogger _logger;

        #endregion

        #region .ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providers"></param>
        /// <param name="logger"></param>
        public WeatherForecastService(IEnumerable<IWeatherForecastProvider> providers, ILogger logger)
        {
            _providers = providers;
            _logger = logger;
        }

        #endregion

        #region IWeatherForecastService

        /// <inheritdoc />
        public async Task<ServiceWeatherForecastModel> GetWeatherForecastAsync(double latitude, double longitude,
          UnitsSystem units = UnitsSystem.Imperial, Language language = Language.English)
        {
            _logger.LogInfo($"{nameof(GetWeatherForecastAsync)} was called with the following parameters: " +
                $"latitude={latitude}, longitude={longitude}, units={units}, language={language}");

            var providerWeatherForecasts = new List<ProviderWeatherForecastModel>();
            var avarageValues = new ProviderWeatherForecastModel();

            foreach (var provider in _providers)
            {
                try
                {
                    var providerWeatherForecast = await provider.GetWeatherForecastAsync(latitude, longitude, units, language);
                    providerWeatherForecasts.Add(providerWeatherForecast);
                }
                catch (ResponseRetrievalException ex)
                {
                    _logger.LogError($"Name: {provider.ServiceName}. Uri: {provider.ServiceUri}", ex);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Weather forecast was not extracted for {provider.ServiceName}.", ex);
                    throw;
                }
            }

            avarageValues.Temperature = providerWeatherForecasts.Average(p => p.Temperature);
            avarageValues.TemperatureMin = providerWeatherForecasts.Where(p => p.TemperatureMin.HasValue).Average(p => p.TemperatureMin);
            avarageValues.TemperatureMax = providerWeatherForecasts.Where(p => p.TemperatureMax.HasValue).Average(p => p.TemperatureMax);
            avarageValues.Humidity = providerWeatherForecasts.Average(p => p.Humidity);
            avarageValues.WindSpeed = providerWeatherForecasts.Average(p => p.WindSpeed);
            avarageValues.Pressure = providerWeatherForecasts.Average(p => p.Pressure);

            return new ServiceWeatherForecastModel
            {
                Providers = providerWeatherForecasts,
                AvarageValues = avarageValues
            };
        }

        #endregion
    }
}
