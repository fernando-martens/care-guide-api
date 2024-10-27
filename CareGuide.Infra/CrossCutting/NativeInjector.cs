using CareGuide.Core.Interfaces;
using CareGuide.Core.Services;
using CareGuide.Data.Interfaces;
using CareGuide.Data.Repositories;
using CareGuide.Security;
using CareGuide.Security.Interfaces;
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
            services.AddScoped<IJwtService, JwtService>();

            #endregion

            #region Repository

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            #endregion

        }

    }
}
