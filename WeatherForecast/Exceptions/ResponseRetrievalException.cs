using System;

namespace WeatherForecast.Exceptions
{
    public class ResponseRetrievalException : Exception
    {
        public ResponseRetrievalException(string message) : base(message)
        {
        }
    }
}
