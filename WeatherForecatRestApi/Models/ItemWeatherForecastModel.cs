using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WeatherForecast.Models;

namespace WeatherForecatRestApi.Models
{
    public class ItemWeatherForecastModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Provider { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitsSystem Units { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Language Language { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Coordinates Coordinates { get; set; }

        public double Temperature { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TemperatureMin { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TemperatureMax { get; set; }

        public double Humidity { get; set; }
        public double Pressure { get; set; }

        public double WindSpeed { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Location { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Summary { get; set; }
    }
}
