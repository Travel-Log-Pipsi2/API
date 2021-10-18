using Core.Interfaces;
using Core.Repositories;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Storage.Models;

namespace Core
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Test>, BaseRepository<Test>>();

            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<ICrudTestService, CrudTestService>();
            
            return services;
        }
    }
}
