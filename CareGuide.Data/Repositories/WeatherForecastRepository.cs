using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;
using System.Linq;

namespace CareGuide.Data.Repositories
{
    public class WeatherForecastRepository: BaseRepository, IWeatherForecastRepository 
    {
        public WeatherForecastRepository(DatabaseContext context) : base(context)
        {
        }

        public void Insert(WeatherForecastTable table)
        {
            _context.Set<WeatherForecastTable>().Add(table);
            _context.SaveChanges();
        }

        public List<WeatherForecastTable> ListAll()
        {
            return _context.Set<WeatherForecastTable>().ToList();
        }  

    }
}
