using Core;
using Core.Interfaces.Authentication;
using Core.Services.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Installers
{
    public class MvcInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ILoggedUserProvider, LoggedUserProvider>();
            services.AddCore();
            services.AddControllers();
            services.AddHttpClient();
        }
    }
}
