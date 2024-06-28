using CareGuide.Models.Tables;

namespace CareGuide.Core.Interfaces
{
    public interface IWeatherForecastService
    {
        void Insert();
        List<WeatherForecastTable> ListAll();
    }
}
