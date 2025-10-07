using CareGuide.Core.Interfaces;
using CareGuide.Core.Services;
using CareGuide.Data.Interfaces;
using CareGuide.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CareGuide.Infra.CrossCutting
{
    public static class NativeInjector
    {

        public static void Register(IServiceCollection services)
        {
            #region Service

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPersonAnnotationService, PersonAnnotationService>();
            services.AddScoped<IPersonHealthService, PersonHealthService>();
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IPersonPhoneService, PersonPhoneService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            #endregion

            #region Repository

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonAnnotationRepository, PersonAnnotationRepository>();
            services.AddScoped<IPersonHealthRepository, PersonHealthRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IPersonPhoneRepository, PersonPhoneRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            #endregion
        }

    }
}
