using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Models.Models;

namespace CareGuide.Core.Services
{
    public class WeatherForecastService: IWeatherForecastService
    {
       
        private readonly IWeatherForecastRepository weatherForecastRepository;

        public WeatherForecastService(IWeatherForecastRepository _weatherForecastRepository)
        {
            weatherForecastRepository = _weatherForecastRepository;
        }

        public List<WeatherForecastModel> ListAll()
        {
            return weatherForecastRepository.ListAll();
        }

    }

}
