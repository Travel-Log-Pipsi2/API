using Core.Interfaces.Authentication;
using Core.Requests;
using Core.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Models.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Authentication
{
    internal class LoginService : AuthServicesProvider, ILoginService
    {
        public LoginService(UserManager<User> userManager, IConfiguration config, IJwtGenerator jwtGenerator)
            : base(userManager, config: config, jwtGenerator: jwtGenerator) { }

        public async Task<ServiceResponse> Login(LoginRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var tokenResponse = _jwtGenerator.GenerateJWTToken(_config, user, roles);

                return ServiceResponse<string>.Success(tokenResponse, "Successful login");
            }

            return ServiceResponse.Error("Email or password is not correct!");
        }
    }
}
