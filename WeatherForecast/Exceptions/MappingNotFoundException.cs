using System;

namespace WeatherForecast.Exceptions
{
    public class MappingNotFoundException : Exception
    {
        public MappingNotFoundException(string message) : base(message)
        {
        }
    }
}
