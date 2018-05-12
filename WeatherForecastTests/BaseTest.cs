using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using WeatherForecatRestApi;

namespace WeatherForecastTests
{
    public class BaseTest
    {
        protected WeatherForecastSecrets Configuration { get; }

        public BaseTest()
        {
            Configuration = GetConfiguration(Path.Combine("..", "..", "..", "..",
                "WeatherForecatRestApi", "appsecrets.json"));
        }

        private WeatherForecastSecrets GetConfiguration(string path)
        {
            var data = File.ReadAllText(path);
            var config = JsonConvert.DeserializeObject<Dictionary<string, WeatherForecastSecrets>>(data);
            return config[nameof(WeatherForecastSecrets)];
        }
    }
}
