using System;

namespace WeatherForecast.Exceptions
{
    public class ResponseParsingException : Exception
    {
        public ResponseParsingException(string message) : base(message)
        {
        }
    }
}
