using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Models;

namespace WeatherForecast.Provider.OpenWeatherMap
{
    /// <summary>
    /// https://openweathermap.org
    /// </summary>
    public class OpenWeatherMapProvider : BaseWeatherForecastProvider, IWeatherForecastProvider
    {
        #region Properties

        /// <inheritdoc />
        public override string ServiceUri { get; } = "http://api.openweathermap.org";

        /// <inheritdoc />
        public string ServiceName { get; } = Name;

        public static string Name { get; } = "OpenWeatherMap";

        protected override Dictionary<UnitsSystem, string> UnitsSystemMappings { get; } =
            new Dictionary<UnitsSystem, string> {
                { UnitsSystem.Imperial, "imperial" },
                { UnitsSystem.Metric, "metric" }
            };

        protected static Dictionary<OpenWeatherLanguage, string> ProviderLanguageMappings { get; } =
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
               { Language.English, ProviderLanguageMappings[OpenWeatherLanguage.English] },
               { Language.Russian, ProviderLanguageMappings[OpenWeatherLanguage.Russian] },
               { Language.Ukrainian, ProviderLanguageMappings[OpenWeatherLanguage.Ukrainian] }
           };

        #endregion

        #region .ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="secretKey"></param>
        public OpenWeatherMapProvider(string secretKey) : base(secretKey)
        {
        }

        #endregion

        #region IWeatherForecastProvider

        /// <inheritdoc />
        public async Task<ProviderWeatherForecastModel> GetWeatherForecastAsync(double latitude, double longitude,
            UnitsSystem units = UnitsSystem.Imperial, Language language = Language.English)
        {
            string unitsSystem = GetUnitsSystem(units);
            string languageValue = GetLanguage(language);

            string uri = $"{ServiceUri}/data/2.5/weather?appid={SecretKey}&lat={latitude}&lon={longitude}&" +
                $"units={unitsSystem}&lang={languageValue}";

            var data = await GetResponseAsync<OpenWeatherMapResponse>(uri).ConfigureAwait(false);

            return new ProviderWeatherForecastModel
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
                Units = units,
                Provider = ServiceName
            };
        }

        #endregion
    }
}
