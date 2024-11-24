using CareGuide.Infra;
using CareGuide.Security.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CareGuide.Tests.Context;

namespace CareGuide.Tests
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            CommomStartupMethods.ConfigureServices(Configuration, services);
            services.AddScoped<IUserSessionContext, UserSessionContextTests>();
        }
    }
}
