using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Installers
{
    public interface Installer
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
