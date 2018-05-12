using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WeatherForecast.Exceptions;
using WeatherForecast.Models;

namespace WeatherForecast.Provider
{
    public abstract class BaseWeatherForecastProvider
    {
        #region Properties

        protected string SecretKey { get; }

        public abstract string ServiceUri { get; }

        protected abstract Dictionary<UnitsSystem, string> UnitsSystemMappings { get; }
        protected abstract Dictionary<Language, string> LanguageMappings { get; }

        #endregion

        #region .ctor

        protected BaseWeatherForecastProvider(string secretKey)
        {
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new Exception("Secret key is not specified.");

            SecretKey = secretKey;
        }

        #endregion

        #region Methods

        protected async static Task<TResponse> GetResponseAsync<TResponse>(string url)
            where TResponse : struct
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(url).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var responseObject = JsonConvert.DeserializeObject<TResponse>(data);
                    return responseObject;
                }
            }

            throw new ResponseRetrievalException("Request has failed.");
        }

        protected string GetUnitsSystem(UnitsSystem units)
        {
            if (!UnitsSystemMappings.TryGetValue(units, out string temperatureScaleValue))
                throw new MappingNotFoundException($"Units system is not found for {units}. Service: {ServiceUri}.");

            return temperatureScaleValue;
        }

        protected string GetLanguage(Language language)
        {
            if (!LanguageMappings.TryGetValue(language, out string languageValue))
                throw new MappingNotFoundException($"Language is not found for {language}. Service: {ServiceUri}.");

            return languageValue;
        }

        #endregion
    }
}
