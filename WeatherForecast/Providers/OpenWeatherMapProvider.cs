using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Models;
using WeatherForecastTestApplication.WeatherForecast;

namespace WeatherForecast.Providers
{
    /// <summary>
    /// https://openweathermap.org
    /// </summary>
    public class OpenWeatherMapProvider : BaseWeatherForecastProvider, IWeatherForecastProvider
    {
        #region Properties

        protected override string ServiceUri { get; } = "api.openweathermap.org";

        protected override Dictionary<TemperatureScale, string> TemperatureMappings { get; } =
            new Dictionary<TemperatureScale, string> {
                { TemperatureScale.Kelvin, "imperial" },
                { TemperatureScale.Celsius, "metric" }
            };

        protected Dictionary<OpenWeatherLanguage, string> ProviderLanguageMappings { get; } =
           new Dictionary<OpenWeatherLanguage, string> {
                { OpenWeatherLanguage.Arabic, "ar" },
                { OpenWeatherLanguage.Bulgarian, "bg" },
                { OpenWeatherLanguage.Catalan, "ca" },
                { OpenWeatherLanguage.Czech, "cz" },
                { OpenWeatherLanguage.German, "de" },
                { OpenWeatherLanguage.Greek, "el" },
                { OpenWeatherLanguage.English, "en" },
                { OpenWeatherLanguage.PersianFarsi, "fa" },
                { OpenWeatherLanguage.Finnish, "fi" },
                { OpenWeatherLanguage.French, "fr" },
                { OpenWeatherLanguage.Galician, "gl" },
                { OpenWeatherLanguage.Croatian, "hr" },
                { OpenWeatherLanguage.Hungarian, "hu" },
                { OpenWeatherLanguage.Italian, "it" },
                { OpenWeatherLanguage.Japanese, "ja" },
                { OpenWeatherLanguage.Korean, "kr" },
                { OpenWeatherLanguage.Latvian, "la" },
                { OpenWeatherLanguage.Lithuanian, "lt" },
                { OpenWeatherLanguage.Macedonian, "mk" },
                { OpenWeatherLanguage.Dutch, "nl" },
                { OpenWeatherLanguage.Polish, "pl" },
                { OpenWeatherLanguage.Portuguese, "pt" },
                { OpenWeatherLanguage.Romanian, "ro" },
                { OpenWeatherLanguage.Russian, "ru" },
                { OpenWeatherLanguage.Swedish, "se" },
                { OpenWeatherLanguage.Slovak, "sk" },
                { OpenWeatherLanguage.Slovenian, "sl" },
                { OpenWeatherLanguage.Spanish, "es" },
                { OpenWeatherLanguage.Turkish, "tr" },
                { OpenWeatherLanguage.Ukrainian, "ua" },
                { OpenWeatherLanguage.Vietnamese, "vi" },
                { OpenWeatherLanguage.ChineseSimplified, "zh_cn" },
                { OpenWeatherLanguage.ChineseTraditional, "zh_tw" }
           };

        protected override Dictionary<Language, string> LanguageMappings { get; } =
           new Dictionary<Language, string>
           {
           };

        #endregion

        #region .ctor

        protected OpenWeatherMapProvider(string secretKey) : base(secretKey)
        {
        }

        #endregion

        #region IWeatherForecastProvider

        public async Task<TemperatureForecastModel> GetTemperatureAsync(double latitude, double longitude,
            TemperatureScale temperatureScale = TemperatureScale.Kelvin, Language language = Language.English)
        {
            string scale = GetTemperatureScale(temperatureScale);
            string languageValue = GetLanguage(language);

            string uri = $"{ServiceUri}/data/2.5/weather?appid={SecretKey}&lat={latitude}&lon={longitude}&units={scale}";

            var data = await GetResponseAsync<OpenWeatherMapResponse>(uri).ConfigureAwait(false);

            return new TemperatureForecastModel
            {
                Coordinates = new Coordinates
                {
                    Latitude = data.coord.lat,
                    Longitude = data.coord.lon
                },
                Temperature = data.main.temp,
                TemperatureMin = data.main.temp_min,
                TemperatureMax = data.main.temp_max,
                Humidity = data.main.humidity,
                Pressure = data.main.pressure,
                WindSpeed = data.wind.speed,
                Summary = data.weather.FirstOrDefault().description,
                Language = language,
                Scale = temperatureScale
            };
        }

        #endregion
    }
}
