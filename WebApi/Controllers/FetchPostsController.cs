using Core.Interfaces;
using Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FetchPostsController : ControllerBase
    {
        readonly IFetchDataService _fetchService;

        public FetchPostsController(IFetchDataService fetchService)
        {
            _fetchService = fetchService;
        }

        [HttpGet]
        [Route("Facebook")]
        public async Task<ServiceResponse> FromFacebook()
        {
            return await _fetchService.Facebook();
        }
    }
}