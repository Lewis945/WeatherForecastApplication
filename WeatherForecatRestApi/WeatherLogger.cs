using System;
using WeatherForecast.Logging;

namespace WeatherForecatRestApi
{
    public class WeatherLogger : ILogger
    {
        public void LogError(string message, Exception exception = null)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(string message)
        {
            throw new NotImplementedException();
        }
    }
}
