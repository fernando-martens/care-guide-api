using CareGuide.Models.Tables;


namespace CareGuide.Data.Interfaces
{
    public interface IWeatherForecastRepository
    {
        Task Insert(WeatherForecastTable table);
        Task<List<WeatherForecastTable>> ListAll();
    }
}
