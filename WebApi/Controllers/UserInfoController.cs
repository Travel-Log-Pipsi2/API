using Core.Interfaces;
using Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : Controller
    {
        private readonly IUserProfileService _service;

        public UserInfoController(IUserProfileService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetInfo")]
        public Task<ServiceResponse> GetInfo()
        {
            return _service.GetInfo();
        }

        [HttpGet]
        [Route("GetInfo/{userId?}")]
        public Task<ServiceResponse> GetInfo(Guid userId)
        {
            return _service.GetInfo(userId);
        }

        [HttpGet]
        [Route("GetStats")]
        public Task<ServiceResponse> GetStats()
        {
            return _service.GetStats();
        }

        [HttpGet]
        [Route("GetStats/{userId?}")]
        public Task<ServiceResponse> GetStats(Guid userId)
        {
            return _service.GetStats(userId);
        }

    }

}
