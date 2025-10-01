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
            #region Core

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPersonAnnotationService, PersonAnnotationService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            #endregion

            #region Repository

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonAnnotationRepository, PersonAnnotationRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            #endregion
        }

    }
}
