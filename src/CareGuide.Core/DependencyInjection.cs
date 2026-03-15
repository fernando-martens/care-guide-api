using CareGuide.Core.Interfaces;
using CareGuide.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CareGuide.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPersonAnnotationService, PersonAnnotationService>();
            services.AddScoped<IPersonHealthService, PersonHealthService>();
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IPersonPhoneService, PersonPhoneService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            return services;
        }
    }
}
