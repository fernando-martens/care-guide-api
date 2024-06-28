
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

        [HttpGet]
        [Route("List")]
        public IActionResult List()
        {
            return Ok(weatherForecastService.ListAll());
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert()
        {
            weatherForecastService.Insert();
            return Ok();
        }
    }
}
