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

        /// <inheritdoc cref="IWeatherForecastProvider" />
        public override string ServiceUri { get; } = "http://api.openweathermap.org";

        /// <inheritdoc />
        public string ServiceName { get; } = Name;

        public static string Name { get; } = "OpenWeatherMap";

        protected override Dictionary<UnitsSystem, string> UnitsSystemMappings { get; } =
            new Dictionary<UnitsSystem, string>
            {
                {UnitsSystem.Imperial, "imperial"},
                {UnitsSystem.Metric, "metric"}
            };

        protected override List<LanguageMapping> LanguageMappings { get; } =
            new List<LanguageMapping>
            {
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.English.ToString(), Value = "en", Language = Language.English },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Russian.ToString(), Value = "ru", Language = Language.Russian },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Ukrainian.ToString(), Value = "ua", Language = Language.Ukrainian },
                
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Arabic.ToString(), Value = "ar", Language = Language.Arabic },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Bulgarian.ToString(), Value = "bg", Language = Language.Bulgarian },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Catalan.ToString(), Value = "ca", Language = Language.Catalan },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Czech.ToString(), Value = "cz", Language = Language.Czech },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.German.ToString(), Value = "de", Language = Language.German },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Greek.ToString(), Value = "el", Language = Language.Greek },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Finnish.ToString(), Value = "fi", Language = Language.Finnish },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.French.ToString(), Value = "fr", Language = Language.French },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Croatian.ToString(), Value = "hr", Language = Language.Croatian },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Hungarian.ToString(), Value = "hu", Language = Language.Hungarian },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Italian.ToString(), Value = "it", Language = Language.Italian },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Japanese.ToString(), Value = "ja", Language = Language.Japanese },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Korean.ToString(), Value = "kr", Language = Language.Korean },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Dutch.ToString(), Value = "nl", Language = Language.Dutch },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Polish.ToString(), Value = "pl", Language = Language.Polish },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Portuguese.ToString(), Value = "pt", Language = Language.Portuguese },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Romanian.ToString(), Value = "ro", Language = Language.Romanian },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Swedish.ToString(), Value = "se", Language = Language.Swedish },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Slovak.ToString(), Value = "sk", Language = Language.Slovak },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Slovenian.ToString(), Value = "sl", Language = Language.Slovenian },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Spanish.ToString(), Value = "es", Language = Language.Spanish },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Turkish.ToString(), Value = "tr", Language = Language.Turkish },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.ChineseSimplified.ToString(), Value = "zh_cn", Language = Language.SimplifiedChinese },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.ChineseTraditional.ToString(), Value = "zh_tw", Language = Language.TraditionalChinese },
                
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.PersianFarsi.ToString(), Value = "fa" },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Galician.ToString(), Value = "gl" },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Latvian.ToString(), Value = "la" },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Lithuanian.ToString(), Value = "lt" },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Macedonian.ToString(), Value = "mk" },
                new LanguageMapping { ProviderLanguage = OpenWeatherLanguage.Vietnamese.ToString(), Value = "vi" }
            };

        #endregion

        #region .ctor

        /// <summary>
        ///  Instantiates external weather forecast provider.
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