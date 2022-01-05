using Core.Interfaces.Auth;
using Core.Interfaces.Authentication;
using Core.Response;
using Core.Services.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public ChallengeResult Request(string provider)
        {
            string redirectUri = _config["External:RedirectUrl"];
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUri);

            return new ChallengeResult(provider, properties) ;
        }

        public async Task<ServiceResponse> Login()
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return ServiceResponse.Error("Error loading external login information", HttpStatusCode.NoContent);
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
            {
                var username = info.Principal.FindFirst(ClaimTypes.Name).Value.Replace(" ", "_");
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
