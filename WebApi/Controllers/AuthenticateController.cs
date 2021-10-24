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

        public AuthenticateController(IAuthenticationService authenticationService)
        {
            _authenticateService = authenticationService;
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
        public async Task<ServiceResponse> ConfirmEmail(string token, string username)
        {
            ConfirmEmailRequest model = new() { Token = token, UserName = username };

            return await _authenticateService.ConfirmEmail(model);
        }
    }
}
