
using CareGuide.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareGuide.API.Controllers
{

    public class WeatherForecastController : BaseApiController
    {

        private readonly IWeatherForecastService weatherForecastService;

        public WeatherForecastController(IWeatherForecastService _weatherForecastService)
        {
            weatherForecastService = _weatherForecastService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            return Ok(weatherForecastService.ListAll());
        }
    }
}
