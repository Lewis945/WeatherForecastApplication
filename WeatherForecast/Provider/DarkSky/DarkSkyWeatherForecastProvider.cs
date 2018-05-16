using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Logging;
using WeatherForecast.Models;

namespace WeatherForecast.Provider.DarkSky
{
    /// <summary>
    /// https://darksky.net
    /// </summary>
    public class DarkSkyWeatherForecastProvider : BaseWeatherForecastProvider, IWeatherForecastProvider
    {
        #region Properties

        /// <inheritdoc cref="IWeatherForecastProvider" />
        public override string ServiceUri { get; } = "https://api.darksky.net";

        /// <inheritdoc />
        public string ServiceName { get; } = Name;

        public static string Name { get; } = "DarkSky";

        protected override Dictionary<UnitsSystem, string> UnitsSystemMappings { get; } =
            new Dictionary<UnitsSystem, string>
            {
                {UnitsSystem.Imperial, "us"},
                {UnitsSystem.Metric, "si"}
            };

        protected override List<LanguageMapping> LanguageMappings { get; } =
            new List<LanguageMapping>
            {
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.English.ToString(), Value = "en", Language = Language.English },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Russian.ToString(), Value = "ru", Language = Language.Russian },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Ukrainian.ToString(), Value = "uk", Language = Language.Ukrainian },
                
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Arabic.ToString(), Value = "ar", Language = Language.Arabic },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Bulgarian.ToString(), Value = "bg", Language = Language.Bulgarian },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Catalan.ToString(), Value = "ca", Language = Language.Catalan },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Czech.ToString(), Value = "cs", Language = Language.Czech },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.German.ToString(), Value = "de", Language = Language.German },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Greek.ToString(), Value = "el", Language = Language.Greek },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Finnish.ToString(), Value = "fi", Language = Language.Finnish },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.French.ToString(), Value = "fr", Language = Language.French },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Croatian.ToString(), Value = "hr", Language = Language.Croatian },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Hungarian.ToString(), Value = "hu", Language = Language.Hungarian },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Italian.ToString(), Value = "it", Language = Language.Italian },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Japanese.ToString(), Value = "ja", Language = Language.Japanese },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Korean.ToString(), Value = "ko", Language = Language.Korean },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Dutch.ToString(), Value = "nl", Language = Language.Dutch },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Polish.ToString(), Value = "pl", Language = Language.Polish },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Portuguese.ToString(), Value = "pt", Language = Language.Portuguese },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Romanian.ToString(), Value = "ro", Language = Language.Romanian },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Swedish.ToString(), Value = "sv", Language = Language.Swedish },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Slovak.ToString(), Value = "sk", Language = Language.Slovak },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Slovenian.ToString(), Value = "sl", Language = Language.Slovenian },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Spanish.ToString(), Value = "es", Language = Language.Spanish },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Turkish.ToString(), Value = "tr", Language = Language.Turkish },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.SimplifiedChinese.ToString(), Value = "zh", Language = Language.SimplifiedChinese },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.TraditionalChinese.ToString(), Value = "zh-tw", Language = Language.TraditionalChinese },

                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Azerbaijani.ToString(), Value = "az" },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Belarusian.ToString(), Value = "be" },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Bosnian.ToString(), Value = "bs" },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Danish.ToString(), Value = "da" },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Estonian.ToString(), Value = "et" },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Indonesian.ToString(), Value = "id" },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Icelandic.ToString(), Value = "is" },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Georgian.ToString(), Value = "ka" },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Cornish.ToString(), Value = "kw" },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.NorwegianBokmalNb.ToString(), Value = "nb" },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.NorwegianBokmalNo.ToString(), Value = "no" },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Serbian.ToString(), Value = "sr" },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.Tetum.ToString(), Value = "tet" },
                new LanguageMapping { ProviderLanguage = DarkSkyLanguage.IgpayAtinlay.ToString(), Value = "x-pig-latin" }
            };
        
        #endregion

        #region .ctor

        /// <summary>
        /// Instantiates external weather forecast provider.
        /// </summary>
        /// <param name="secretKey"></param>
        public DarkSkyWeatherForecastProvider(string secretKey) : base(secretKey)
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

            string uri = $"{ServiceUri}/forecast/{SecretKey}/{latitude},{longitude}?" +
                         $"exclude=minutely,hourly,daily,alerts,flags&units={unitsSystem}&lang={languageValue}";

            var data = await GetResponseAsync<DarkSkyResponse>(uri).ConfigureAwait(false);

            return new ProviderWeatherForecastModel
            {
                Coordinates = new Coordinates
                {
                    Latitude = data.latitude,
                    Longitude = data.longitude
                },
                Temperature = data.currently.temperature,
                TemperatureMin = null,
                TemperatureMax = null,
                Humidity = data.currently.humidity,
                Pressure = data.currently.pressure,
                WindSpeed = data.currently.windSpeed,
                Summary = data.currently.summary,
                Language = language,
                Units = units,
                Provider = ServiceName
            };
        }

        #endregion
    }
}