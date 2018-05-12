using FluentAssertions;
using System.Threading.Tasks;
using WeatherForecast.Provider;
using WeatherForecast.Provider.OpenWeatherMap;
using Xunit;

namespace WeatherForecastTests.Provider
{
    public class OpenWeatherMapProviderTests : BaseTest
    {
        private readonly IWeatherForecastProvider _provider;

        public OpenWeatherMapProviderTests()
        {
            _provider = new OpenWeatherMapProvider(Configuration.OpenWeatherMap);
        }

        [Fact]
        public async Task Should_Not_Fail()
        {
            double latitude = 58.3607;
            double longitude = 26.7278;

            var forecast = await _provider.GetWeatherForecastAsync(latitude, longitude);

            forecast.Temperature.Should().BeGreaterThan(0);
            forecast.TemperatureMin.Should().NotBeNull();
            forecast.TemperatureMin.Should().BeGreaterThan(0);
            forecast.TemperatureMax.Should().NotBeNull();
            forecast.TemperatureMax.Should().BeGreaterThan(0);
            forecast.Humidity.Should().BeGreaterThan(0);
            forecast.WindSpeed.Should().BeGreaterThan(0);
            forecast.Pressure.Should().BeGreaterThan(0);
            forecast.Summary.Length.Should().BeGreaterThan(0);
        }
    }
}
