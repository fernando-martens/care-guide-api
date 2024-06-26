using CareGuide.Models.Models;


namespace CareGuide.Data.Interfaces
{
    public interface IWeatherForecastRepository
    {
        List<WeatherForecastModel> ListAll();
    }
}
