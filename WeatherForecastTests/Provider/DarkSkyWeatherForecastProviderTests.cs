using FluentAssertions;
using System.Threading.Tasks;
using WeatherForecast.Provider;
using WeatherForecast.Provider.DarkSky;
using Xunit;

namespace WeatherForecastTests.Provider
{
    public class DarkSkyWeatherForecastProviderTests : BaseTest
    {
        private readonly IWeatherForecastProvider _provider;

        public DarkSkyWeatherForecastProviderTests()
        {
            _provider = new DarkSkyWeatherForecastProvider(Configuration.DarkSky);
        }

        [Fact]
        public async Task Should_Not_Fail()
        {
            double latitude = 58.3607;
            double longitude = 26.7278;

            var forecast = await _provider.GetWeatherForecastAsync(latitude, longitude);

            forecast.Temperature.Should().BeGreaterThan(0);
            forecast.TemperatureMin.Should().BeNull();
            forecast.TemperatureMax.Should().BeNull();
            forecast.Humidity.Should().BeGreaterThan(0);
            forecast.WindSpeed.Should().BeGreaterThan(0);
            forecast.Pressure.Should().BeGreaterThan(0);
            forecast.Summary.Length.Should().BeGreaterThan(0);
        }
    }
}
