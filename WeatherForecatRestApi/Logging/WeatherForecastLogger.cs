using System;
using Microsoft.Extensions.Logging;

namespace WeatherForecatRestApi.Logging
{
    public class WeatherForecastLogger : WeatherForecast.Logging.ILogger
    {
        #region Fields

        private readonly ILogger _logger;

        #endregion

        #region .ctor

        public WeatherForecastLogger(ILogger logger)
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