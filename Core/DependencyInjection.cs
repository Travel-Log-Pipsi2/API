using Core.Common;
using Core.Interfaces;
using Core.Interfaces.Auth;
using Core.Interfaces.Authentication;
using Core.Interfaces.Email;
using Core.Repositories;
using Core.Services;
using Core.Services.Auth;
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
            services.AddScoped<IBaseRepository<Marker>, BaseRepository<Marker>>();

            services.AddScoped<ITestRepository, TestRepository>();

            services.AddScoped<ICrudTestService, CrudTestService>();

            services.AddScoped<IMarkerRepository, MarkerRepository>();
            services.AddScoped<IMarkerCrudService, MarkerCrudService>();

            services.AddScoped<IFriendshipRepository, FriendshipRepository>();
            services.AddScoped<IFriendshipService, FriendshipService>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IPasswordResetService, PasswordResetService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAdditionalAuthMetods, AdditionalAuthMetods>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IExternalLoginService, ExternalLoginService>();

            services.AddScoped<IFetchDataService, FetchDataService>();
            services.AddScoped<IConnectionRepository, ConnectionRepository>();

            return services;
        }
    }
}
