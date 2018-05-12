using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast;
using WeatherForecast.Logging;
using WeatherForecast.Provider;
using WeatherForecast.Provider.DarkSky;
using WeatherForecast.Provider.OpenWeatherMap;
using Xunit;

namespace WeatherForecastTests
{
    public class WeatherForecastServiceTests : BaseTest
    {
        private readonly IWeatherForecastService _service;
        private readonly Mock<ILogger> _logger;

        public WeatherForecastServiceTests()
        {
            _logger = new Mock<ILogger>();

            _logger.Setup(l => l.LogError(It.IsAny<string>(), It.IsAny<Exception>()));
            _logger.Setup(l => l.LogInfo(It.IsAny<string>()));

            _service = new WeatherForecastService(new List<IWeatherForecastProvider> {
                new DarkSkyWeatherForecastProvider(Configuration.DarkSky),
                new OpenWeatherMapProvider(Configuration.OpenWeatherMap)
            }, _logger.Object);
        }

        [Fact]
        public async Task Should_Not_Fail()
        {
            double latitude = 58.3607;
            double longitude = 26.7278;

            var forecast = await _service.GetWeatherForecastAsync(latitude, longitude);

            forecast.Providers.Count.Should().Be(2);
            forecast.AvarageValues.Temperature.Should().BeGreaterThan(0);
            forecast.AvarageValues.TemperatureMin.Should().NotBeNull();
            forecast.AvarageValues.TemperatureMin.Should().BeGreaterThan(0);
            forecast.AvarageValues.TemperatureMax.Should().NotBeNull();
            forecast.AvarageValues.TemperatureMax.Should().BeGreaterThan(0);
            forecast.AvarageValues.Humidity.Should().BeGreaterThan(0);
            forecast.AvarageValues.WindSpeed.Should().BeGreaterThan(0);
            forecast.AvarageValues.Pressure.Should().BeGreaterThan(0);
            forecast.AvarageValues.Summary.Should().BeNullOrEmpty();

            _logger.Verify(l => l.LogError(It.IsAny<string>(), It.IsAny<Exception>()), Times.Exactly(0));
        }
    }
}
