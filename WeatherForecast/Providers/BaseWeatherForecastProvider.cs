using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WeatherForecast.Models;

namespace WeatherForecast.Providers
{
    public abstract class BaseWeatherForecastProvider
    {
        #region Properties

        protected string SecretKey { get; }

        protected abstract string ServiceUri { get; }

        protected abstract Dictionary<TemperatureScale, string> TemperatureMappings { get; }
        protected abstract Dictionary<Language, string> LanguageMappings { get; }

        #endregion

        #region .ctor

        protected BaseWeatherForecastProvider(string secretKey)
        {
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
                    var data = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<TResponse>(data);
                    return responseObject;
                }
            }

            throw new Exception("Request has failed.");
        }

        protected string GetTemperatureScale(TemperatureScale temperatureScale)
        {
            if (!TemperatureMappings.TryGetValue(temperatureScale, out string temperatureScaleValue))
                throw new Exception($"Temperature unit is not found for {temperatureScale}. Service: {ServiceUri}.");

            return temperatureScaleValue;
        }

        protected string GetLanguage(Language language)
        {
            if (!LanguageMappings.TryGetValue(language, out string languageValue))
                throw new Exception($"Language is not found for {language}. Service: {ServiceUri}.");

            return languageValue;
        }

        #endregion
    }
}
