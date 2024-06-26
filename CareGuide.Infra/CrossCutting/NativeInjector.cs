using CareGuide.Core.Interfaces;
using CareGuide.Core.Services;
using CareGuide.Data.Repositories;
using CareGuide.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CareGuide.Infra.CrossCutting
{
    public static class NativeInjector
    {

        public static void Register(IServiceCollection services)
        {
            #region Core

            services.AddScoped<IWeatherForecastService, WeatherForecastService>();

            #endregion

            #region Repository

            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

            #endregion

        }

    }
}
