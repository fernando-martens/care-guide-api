using CareGuide.Models.Models;

namespace CareGuide.Core.Interfaces
{
    public interface IWeatherForecastService
    {
        List<WeatherForecastModel> ListAll();
    }
}
