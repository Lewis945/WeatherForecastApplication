using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Models;
using WeatherForecastTestApplication.WeatherForecast;

namespace WeatherForecast.Providers
{
    /// <summary>
    /// https://darksky.net
    /// </summary>
    public class DarkSkyWeatherForecastProvider : BaseWeatherForecastProvider, IWeatherForecastProvider
    {
        #region Properties

        protected override string ServiceUri { get; } = "https://api.darksky.net";

        protected override Dictionary<TemperatureScale, string> TemperatureMappings { get; } =
           new Dictionary<TemperatureScale, string> {
                { TemperatureScale.Kelvin, "us" },
                { TemperatureScale.Celsius, "si" }
           };

        protected Dictionary<DarkSkyLanguage, string> ProviderLanguageMappings { get; } =
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
           };

        #endregion

        #region .ctor

        protected DarkSkyWeatherForecastProvider(string secretKey) : base(secretKey)
        {
        }

        #endregion

        #region IWeatherForecastProvider

        public async Task<TemperatureForecastModel> GetTemperatureAsync(double latitude, double longitude,
           TemperatureScale temperatureScale = TemperatureScale.Kelvin, Language language = Language.English)
        {
            string scale = GetTemperatureScale(temperatureScale);
            string languageValue = GetLanguage(language);

            string uri = $"{ServiceUri}/forecast/{SecretKey}/{latitude},{longitude}?exclude=currently,minutely,hourly,alerts,flags";

            var data = await GetResponseAsync<DarkSkyResponse>(uri).ConfigureAwait(false);

            return new TemperatureForecastModel
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
                Scale = temperatureScale
            };
        }

        #endregion
    }
}
