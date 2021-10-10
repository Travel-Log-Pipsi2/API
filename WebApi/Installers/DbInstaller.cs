using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.DataAccessLayer;
using System.Text;

namespace WebApi.Installers
{
    public class DbInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var config = new StringBuilder(configuration["ConnectionStrings:ApiConntectionString"]);
            string connection = config.Replace("ENVPW", configuration["DB_PASSWORD"])
                .Replace("ENVID", configuration["DB_USER_ID"])
                .ToString();

            services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connection));
        }
    }
}
