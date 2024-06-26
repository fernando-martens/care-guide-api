using CareGuide.Data.Interfaces;
using CareGuide.Models.Models;

namespace CareGuide.Data.Repositories
{
    public class WeatherForecastRepository: IWeatherForecastRepository
    {
        public WeatherForecastRepository()
        {
        }

        public List<WeatherForecastModel> ListAll()
        {

            List<WeatherForecastModel> list = new List<WeatherForecastModel>()
            {
                new WeatherForecastModel()
                {
                    Date = DateTime.Now.Date,
                    Summary = "Test 1 "
                },
                new WeatherForecastModel()
                {
                    Date = DateTime.Now.Date.AddDays(20),
                    Summary = "Test 2 "
                }
            };

            return list;

        }
      

    }
}
