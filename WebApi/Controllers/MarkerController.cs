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
    public class MarkerController : Controller
    {
        private readonly IMarkerCrudService _service;

        public MarkerController(IMarkerCrudService service)
        {
            _service = service;
        }

        [HttpGet]
        public Task<ServiceResponse> Index()
        {
            return _service.GetMarkers();
        }

        [HttpPost()]
        public Task<ServiceResponse> Create()
        {
            return _service.CreateMarker();
        }
    }

}
