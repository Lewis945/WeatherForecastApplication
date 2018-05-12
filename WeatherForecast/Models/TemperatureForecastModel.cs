namespace WeatherForecast.Models
{
    public class TemperatureForecastModel
    {
        public TemperatureScale Scale { get; set; }
        public Language Language { get; set; }
        public Coordinates Coordinates { get; set; }

        public double Temperature { get; set; }
        public double? TemperatureMin { get; set; }
        public double? TemperatureMax { get; set; }

        public double Humidity { get; set; }
        public double Pressure { get; set; }

        public double WindSpeed { get; set; }

        public string Location { get; set; }
        public string Summary { get; set; }
    }
}
