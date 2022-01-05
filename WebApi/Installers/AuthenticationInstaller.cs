using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Storage.DataAccessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Storage.Models.Identity;

namespace WebApi.Installers
{
    public class AuthenticationInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole<Guid>>(opttion =>
            {
                opttion.Password.RequireDigit = true;
                opttion.Password.RequireLowercase = true;
                opttion.Password.RequireUppercase = true;
                opttion.Password.RequiredLength = 8;
                opttion.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApiDbContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

                    });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddFacebook(options =>
            {
                options.AppId = configuration["Facebook:AppId"];
                options.AppSecret = configuration["Facebook:Secret"];
            })
            .AddInstagram(options =>
            {
                options.ClientId = configuration["Instagram:ClientId"];
                options.ClientSecret = configuration["Instagram:Secret"];
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidAudience = configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });
        }
    }
}