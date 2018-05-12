namespace WeatherForecast.Models
{
    public struct OpenWeatherMapResponse
    {
        public struct Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public struct Weather
        {
            public string description { get; set; }
        }

        public struct Main
        {
            public double temp { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
        }

        public struct Wind
        {
            public double speed { get; set; }
        }

        public Coord coord { get; set; }
        public Weather[] weather { get; set; }

        public Main main { get; set; }

        public Wind wind { get; set; }

        public string name { get; set; }
    }
}
