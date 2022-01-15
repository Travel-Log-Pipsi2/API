using Core.Interfaces;
using Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FetchPostsController : ControllerBase
    {

        readonly IFetchPostsService _fetchService;

        public FetchPostsController(IFetchPostsService fetchService)
        {
            _fetchService = fetchService;
        }

        [HttpGet]
        [Route("Facebook")]
        public async Task<ServiceResponse> FromFacebook()
        {
            return await _fetchService.Facebook();
        }

        [HttpGet]
        [Route("Facebook/Connect")]
        public async Task<ServiceResponse> FacebookConnect(string accessToken, string userProviderId)
        {
            return await _fetchService.Connect(accessToken, userProviderId);
        }
    }
}