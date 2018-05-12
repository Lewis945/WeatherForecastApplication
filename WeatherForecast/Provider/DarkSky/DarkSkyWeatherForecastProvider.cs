using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Models;

namespace WeatherForecast.Provider.DarkSky
{
    /// <summary>
    /// https://darksky.net
    /// </summary>
    public class DarkSkyWeatherForecastProvider : BaseWeatherForecastProvider, IWeatherForecastProvider
    {
        #region Properties

        /// <inheritdoc />
        public override string ServiceUri { get; } = "https://api.darksky.net";

        /// <inheritdoc />
        public string ServiceName { get; } = Name;

        public static string Name { get; } = "DarkSky";

        protected override Dictionary<UnitsSystem, string> UnitsSystemMappings { get; } =
           new Dictionary<UnitsSystem, string> {
                { UnitsSystem.Imperial, "us" },
                { UnitsSystem.Metric, "si" }
           };

        protected static Dictionary<DarkSkyLanguage, string> ProviderLanguageMappings { get; } =
           new Dictionary<DarkSkyLanguage, string> {
                { DarkSkyLanguage.Arabic, "ar" },
                { DarkSkyLanguage.Azerbaijani, "az" },
                { DarkSkyLanguage.Belarusian, "be" },
                { DarkSkyLanguage.Bulgarian, "bg" },
                { DarkSkyLanguage.Bosnian, "bs" },
                { DarkSkyLanguage.Catalan, "ca" },
                { DarkSkyLanguage.Czech, "cs" },
                { DarkSkyLanguage.Danish, "da" },
                { DarkSkyLanguage.German, "de" },
                { DarkSkyLanguage.Greek, "el" },
                { DarkSkyLanguage.English, "en" },
                { DarkSkyLanguage.Spanish, "es" },
                { DarkSkyLanguage.Estonian, "et" },
                { DarkSkyLanguage.Finnish, "fi" },
                { DarkSkyLanguage.French, "fr" },
                { DarkSkyLanguage.Croatian, "hr" },
                { DarkSkyLanguage.Hungarian, "hu" },
                { DarkSkyLanguage.Indonesian, "id" },
                { DarkSkyLanguage.Icelandic, "is" },
                { DarkSkyLanguage.Italian, "it" },
                { DarkSkyLanguage.Japanese, "ja" },
                { DarkSkyLanguage.Georgian, "ka" },
                { DarkSkyLanguage.Korean, "ko" },
                { DarkSkyLanguage.Cornish, "kw" },
                { DarkSkyLanguage.NorwegianBokmalNB, "nb" },
                { DarkSkyLanguage.Dutch, "nl" },
                { DarkSkyLanguage.NorwegianBokmalNO, "no" },
                { DarkSkyLanguage.Polish, "pl" },
                { DarkSkyLanguage.Portuguese, "pt" },
                { DarkSkyLanguage.Romanian, "ro" },
                { DarkSkyLanguage.Russian, "ru" },
                { DarkSkyLanguage.Slovak, "sk" },
                { DarkSkyLanguage.Slovenian, "sl" },
                { DarkSkyLanguage.Serbian, "sr" },
                { DarkSkyLanguage.Swedish, "sv" },
                { DarkSkyLanguage.Tetum, "tet" },
                { DarkSkyLanguage.Turkish, "tr" },
                { DarkSkyLanguage.Ukrainian, "uk" },
                { DarkSkyLanguage.IgpayAtinlay, "x-pig-latin" },
                { DarkSkyLanguage.SimplifiedChinese, "zh" },
                { DarkSkyLanguage.TraditionalChinese, "zh-tw" }
           };

        protected override Dictionary<Language, string> LanguageMappings { get; } =
           new Dictionary<Language, string>
           {
               { Language.English, ProviderLanguageMappings[DarkSkyLanguage.English] },
               { Language.Russian, ProviderLanguageMappings[DarkSkyLanguage.Russian] },
               { Language.Ukrainian, ProviderLanguageMappings[DarkSkyLanguage.Ukrainian] }
           };

        #endregion

        #region .ctor

        /// <summary>
        /// 
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
