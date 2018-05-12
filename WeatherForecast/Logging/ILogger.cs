using System;

namespace WeatherForecast.Logging
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message, Exception exception = null);
    }
}
