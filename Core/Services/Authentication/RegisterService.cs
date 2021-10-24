using Core.Common;
using Core.Interfaces.Authentication;
using Core.Interfaces.Email;
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
        public RegisterService(UserManager<User> userManager, IConfiguration config, IEmailService emailService, IAdditionalAuthMetods additionalAuthMethods)
            : base(userManager, config: config, emailService: emailService)
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

                user = await _userManager.FindByNameAsync(user.UserName);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var urlString = _additionalAuthMetods.BuildUrl(token, user.UserName, _config["Paths:ConfirmEmail"]);

                await _emailService.SendEmailAsync(user.Email, "Confirm your email address", SettingsVariables.GetHtmlConfirmEmailPage(urlString));

                return ServiceResponse.Success("User created successfully! Confirm your email.", HttpStatusCode.Created);
            }
            catch
            {
                return ServiceResponse.Error("An error accured while creating account.");
            }
        }

        public async Task<ServiceResponse> ConfirmEmail(ConfirmEmailRequest model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var isConfirmed = user.EmailConfirmed;
                var result = await _userManager.ConfirmEmailAsync(user, model.Token);

                if (isConfirmed || !result.Succeeded)
                    throw new();

                return ServiceResponse.Success("Email confirmed succesfully");
            }
            catch
            {
                return ServiceResponse.Error("Link is invalid", HttpStatusCode.BadRequest);
            }
        }
    }
}
