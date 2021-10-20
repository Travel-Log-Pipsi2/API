using Core.Interfaces.Authentication;
using Core.Requests;
using Core.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Models.Identity;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services.Authentication
{
    internal class RegisterService : AuthServicesProvider, IRegisterService
    {
        private readonly IAdditionalAuthMetods _additionalAuthMetods;
        public RegisterService(UserManager<User> userManager, IConfiguration config, IAdditionalAuthMetods additionalAuthMethods)
            : base(userManager, config: config)
        {
            _additionalAuthMetods = additionalAuthMethods;
        }

        public async Task<ServiceResponse> Register(RegisterRequest model)
        {
            try
            {
                if (await _userManager.FindByNameAsync(model.Username) != null || await _userManager.FindByEmailAsync(model.Email) != null)
                    return ServiceResponse.Error("Account already exists!");

                User user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return ServiceResponse.Error(_additionalAuthMetods.CreateValidationErrorMessage(result));

                await _userManager.AddToRoleAsync(user, UserRoles.User);

                return ServiceResponse.Success("User created successfully!", HttpStatusCode.Created);
            }
            catch
            {
                return ServiceResponse.Error("An error accured while creating account.");
            }
        }
    }
}
