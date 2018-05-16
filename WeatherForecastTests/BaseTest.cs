using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using WeatherForecatRestApi.Models;

namespace WeatherForecastTests
{
    public class BaseTest
    {
        protected WeatherForecastSecretsModel Configuration { get; }

        public BaseTest()
        {
            Configuration = GetConfiguration(Path.Combine("..", "..", "..", "..",
                "WeatherForecatRestApi", "appsecrets.json"));
        }

        private WeatherForecastSecretsModel GetConfiguration(string path)
        {
            var data = File.ReadAllText(path);
            var config = JsonConvert.DeserializeObject<Dictionary<string, WeatherForecastSecretsModel>>(data);
            return config[nameof(WeatherForecastSecretsModel).Replace("Model", string.Empty)];
        }
    }
}
