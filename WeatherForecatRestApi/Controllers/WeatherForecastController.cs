using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WeatherForecast;
using WeatherForecast.Models;
using WeatherForecatRestApi.Models;

namespace WeatherForecatRestApi.Controllers
{
    [Route("api/[controller]")]
    public class WeatherForecastController : Controller
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IWeatherForecastService _weatherForecastService;

        #endregion

        #region .ctor

        public WeatherForecastController(IMapper mapper, ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _mapper = mapper;
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        #endregion

        #region Methods

        // GET api/weatherforecast/providers
        [HttpGet("providers")]
        public IActionResult GetProviders()
        {
            var providers = _weatherForecastService.Providers;
            _logger.LogInformation($"Request for providers. They are {string.Join(",", providers)}.");
            return Json(providers);
        }

        // GET api/weatherforecast?latitude=58.3607&longitude=26.7278&scale=Kelvin&language=English
        [HttpGet]
        public async Task<IActionResult> GetWeatherForecast(double? latitude, double? longitude,
            UnitsSystem units = UnitsSystem.Imperial, Language language = Language.English)
        {
            _logger.LogInformation($"Request for weather forecast. Latitude={latitude}, longitude={longitude}, units={units}, language={language}");

            if (!latitude.HasValue)
                return BadRequest("Latitude is not specified.");

            if (!longitude.HasValue)
                return BadRequest("Longitude is not specified.");

            var weatherForecast = await _weatherForecastService.GetWeatherForecastAsync(
                latitude.Value, longitude.Value, units, language);

            _logger.LogInformation($"Weather forecast: {JsonConvert.SerializeObject(weatherForecast)}");

            return Json(_mapper.Map<WeatherForecastModel>(weatherForecast));
        }

        #endregion
    }
}
