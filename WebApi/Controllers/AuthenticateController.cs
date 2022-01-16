using Core.Interfaces.Authentication;
using Core.Requests;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationService _authenticateService;
        private readonly IPasswordResetService _passwordResetService;

        public AuthenticateController(IAuthenticationService authenticationService, IPasswordResetService passwordResetService)
        {
            _authenticateService = authenticationService;
            _passwordResetService = passwordResetService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ServiceResponse> Login([FromBody] LoginRequest model)
        {
            return await _authenticateService.Login(model);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ServiceResponse> Register([FromBody] RegisterRequest model)
        {
            return await _authenticateService.Register(model);
        }

        [HttpGet]
        [Route("Confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            ConfirmEmailRequest model = new() { Token = token, Email = email };

            await _authenticateService.ConfirmEmail(model);
            return Redirect("https://frosty-leakey-61ac66.netlify.app/login");
        }

        [HttpPost]
        [Route("Forgot-password")]
        public async Task<ServiceResponse> ForgotPassword(string email)
        {
            return await _passwordResetService.SendResetPasswordEmail(email);
        }

        [HttpPost]
        [Route("Reset-password")]
        public async Task<ServiceResponse> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            return await _passwordResetService.ResetPassword(model);
        }
    }
}
