using CareGuide.Models.Tables;


namespace CareGuide.Data.Interfaces
{
    public interface IWeatherForecastRepository
    {
        void Insert(WeatherForecastTable table);
        List<WeatherForecastTable> ListAll();
    }
}
