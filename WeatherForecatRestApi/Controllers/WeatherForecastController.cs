using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IWeatherForecastService _weatherForecastService;

        #endregion

        #region .ctor

        public WeatherForecastController(IMapper mapper, IWeatherForecastService weatherForecastService)
        {
            _mapper = mapper;
            _weatherForecastService = weatherForecastService;
        }

        #endregion

        #region Methods

        // GET api/weatherforecast/providers
        [HttpGet("providers")]
        public IActionResult GetProviders()
        {
            return Json(_weatherForecastService.Providers);
        }

        // GET api/weatherforecast?latitude=58.3607&longitude=26.7278&scale=Kelvin&language=English
        [HttpGet]
        public async Task<IActionResult> Get(double? latitude, double? longitude,
            UnitsSystem units = UnitsSystem.Imperial, Language language = Language.English)
        {
            if (!latitude.HasValue)
                return BadRequest("Latitude is not specified.");

            if (!longitude.HasValue)
                return BadRequest("Longitude is not specified.");

            var weatherForecast = await _weatherForecastService.GetWeatherForecastAsync(
                latitude.Value, longitude.Value, units, language);

            return Json(_mapper.Map<WeatherForecastModel>(weatherForecast));
        }

        #endregion
    }
}
