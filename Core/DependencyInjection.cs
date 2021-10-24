using Core.Common;
using Core.Interfaces;
using Core.Interfaces.Authentication;
using Core.Interfaces.Email;
using Core.Repositories;
using Core.Services;
using Core.Services.Authentication;
using Core.Services.Email;
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

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAdditionalAuthMetods, AdditionalAuthMetods>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
