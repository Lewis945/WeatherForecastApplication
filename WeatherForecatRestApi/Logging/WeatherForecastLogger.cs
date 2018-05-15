using Microsoft.Extensions.Logging;
using System;

namespace WeatherForecatRestApi.Logging
{
    public class WeatherForecastLogger : WeatherForecast.Logging.ILogger
    {
        #region Fields

        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        #endregion

        #region .ctor

        public WeatherForecastLogger(Microsoft.Extensions.Logging.ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region ILogger

        public void LogError(string message, Exception exception = null)
        {
            _logger.LogError(exception, message);
        }

        public void LogInfo(string message)
        {
            _logger.LogInformation(message);
        }

        #endregion
    }
}
