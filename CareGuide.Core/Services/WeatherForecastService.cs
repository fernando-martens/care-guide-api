using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;

namespace CareGuide.Core.Services
{
    public class WeatherForecastService: IWeatherForecastService
    {
       
        private readonly IWeatherForecastRepository weatherForecastRepository;

        public WeatherForecastService(IWeatherForecastRepository _weatherForecastRepository)
        {
            weatherForecastRepository = _weatherForecastRepository;
        }

        public void Insert()
        {
            weatherForecastRepository.Insert(new WeatherForecastTable()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now.ToUniversalTime(),
                Summary = "Test"
            });
        }

        public List<WeatherForecastTable> ListAll()
        {
            return weatherForecastRepository.ListAll();
        }

    }

}
