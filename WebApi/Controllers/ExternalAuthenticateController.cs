using Core.Interfaces.Auth;
using Core.Requests;
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

        [HttpPost]
        [Route("Facebook-login")]
        public async Task<ServiceResponse> FacebookLogin([FromBody] FacebookAuthRequest request)
        {
            return await _externalLoginService.FacebookLogin(request);
        }
    }
}