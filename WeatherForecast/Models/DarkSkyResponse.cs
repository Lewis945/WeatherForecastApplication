namespace WeatherForecast.Models
{
    public struct DarkSkyResponse
    {
        public struct DarkSkyCurrentlyResponse
        {
            public double temperature { get; set; }
            public double humidity { get; set; }
            public double pressure { get; set; }
            public double windSpeed { get; set; }

            public string summary { get; set; }
        }

        public double latitude { get; set; }
        public double longitude { get; set; }

        public DarkSkyCurrentlyResponse currently { get; set; }
    }
}
