using Core.Interfaces.Auth;
using Core.Interfaces.Authentication;
using Core.Requests;
using Core.Response;
using Core.Services.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Models.Identity;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Services.Auth
{
    class ExternalLoginService : AuthServicesProvider, IExternalLoginService
    {
        private readonly IAdditionalAuthMetods _additionalAuthMetods;
        public ExternalLoginService(UserManager<User> _userManager, SignInManager<User> _signInManager, IConfiguration _config, IJwtGenerator _jwtGenerator, IAdditionalAuthMetods additionalAuthMethods) 
            : base(_userManager, _signInManager, config: _config, jwtGenerator: _jwtGenerator)
        {
            _additionalAuthMetods = additionalAuthMethods;
        }

        public async Task<ServiceResponse> Login(FacebookAuthRequest request)
        {
            
            ExternalLoginInfo info = new(new ClaimsPrincipal(), request.GraphDomain, request.UserId, request.Name);
            if (info == null)
            {
                return ServiceResponse.Error("Error loading external login information", HttpStatusCode.NoContent);
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
            {
                return await _additionalAuthMetods.GetUserTokenResponse(info.LoginProvider, info.ProviderKey);
            }

            User user = CreateExternalUser(info);
            IdentityResult identResult = await _userManager.CreateAsync(user);
           
            if (identResult.Succeeded)
            {
                identResult = await _userManager.AddLoginAsync(user, info);
                await _userManager.AddToRoleAsync(user, UserRoles.User);
                if (identResult.Succeeded)
                {
                    return await _additionalAuthMetods.GetUserTokenResponse(info.LoginProvider, info.ProviderKey);
                }
            }

            return ServiceResponse.Error("User not created", HttpStatusCode.UnprocessableEntity);
        }

        private User CreateExternalUser(ExternalLoginInfo info)
        {
            User user = new()
            {
                UserName = info.Principal.FindFirst(ClaimTypes.Name).Value.Replace(" ", "_"),
                EmailConfirmed = true
            };
            
            return user;
        }
    }
}
