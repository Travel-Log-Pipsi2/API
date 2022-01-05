using Core.Interfaces.Auth;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalAuthenticateController : ControllerBase
    {
        private readonly IExternalLoginService _externalLoginService;

        public ExternalAuthenticateController(IExternalLoginService externalLoginService)
        {
            _externalLoginService = externalLoginService;
        }

        [HttpGet]
        [Route("External-login")]
        public IActionResult ExternalLogin(string provider)
        {
            return _externalLoginService.Request(provider);
        }

        [HttpGet]
        [Route("External-response")]
        public async Task<ServiceResponse> ExternalResponse()
        {
            return await _externalLoginService.Login();
        }
    }
}
