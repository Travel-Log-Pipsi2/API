using Core.Common;
using Core.Interfaces.Authentication;
using Core.Interfaces.Email;
using Core.Requests;
using Core.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Models.Identity;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services.Authentication
{
    internal class PasswordResetService : AuthServicesProvider, IPasswordResetService
    {
        private readonly IAdditionalAuthMetods _additionalAuthMetods;

        public PasswordResetService(UserManager<User> userManager, IConfiguration config, IEmailService emailService, IAdditionalAuthMetods additionalAuthMethods)
            : base(userManager, config: config, emailService: emailService)
        {
            _additionalAuthMetods = additionalAuthMethods;
        }

        public async Task<ServiceResponse> SendResetPasswordEmail(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var urlString = _additionalAuthMetods.BuildUrl(token, user.Email, _config["Paths:ResetPassword"]);

                await _emailService.SendEmailAsync(user.Email, "Reset your password", SettingsVariables.GetHtmlResetPasswordPage(urlString));
            }
            catch
            {
                return ServiceResponse.Error("Sending an e-mail failed");
            }

            return ServiceResponse.Success("Email sent successfully");
        }

        public async Task<ServiceResponse> ResetPassword(ResetPasswordRequest model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.newPassword);

                if (!result.Succeeded)
                    return ServiceResponse.Error(_additionalAuthMetods.CreateValidationErrorMessage(result));

                return ServiceResponse.Success("Password changed succesfully");
            }
            catch
            {
                return ServiceResponse.Error("Password not changed.", HttpStatusCode.UnprocessableEntity);
            }
        }
    }
}
