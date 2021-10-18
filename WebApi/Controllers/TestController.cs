using Core.Interfaces;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly ICrudTestService _service;

        public TestController(ICrudTestService service)
        {
            _service = service;
        }

        [HttpGet]
        public Task<ServiceResponse> Index()
        {
            return _service.GetTests();
        }

        [HttpPost()]
        public Task<ServiceResponse> Create(string value)
        {
            return _service.CreateTest(value);
        }
    }

}
