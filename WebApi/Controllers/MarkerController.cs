using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using System;
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
        [Route("GetMarkers")]
        public Task<ServiceResponse> Get()
        {
            return _service.GetMarkers();
        }

        [HttpGet]
        [Route("GetMarkers/{userId?}")]
        public Task<ServiceResponse> GetFiltered(Guid userId)
        {
            return _service.GetMarkersOfUser(userId);
        }


        [HttpPost()]
        [Route("CreateMarker")]
        public Task<ServiceResponse> Create([FromBody] MarkerRequest model)
        {
            return _service.CreateMarker(model);
        }

        [HttpPost()]
        [Route("UpdateMarker")]
        public Task<ServiceResponse> Update(int MarkerID, [FromBody] MarkerRequest model)
        {
            return _service.UpdateMarker(MarkerID, model);
        }

        [HttpPost()]
        [Route("DeleteMarker")]
        public Task<ServiceResponse> Delete(int MarkerID)
        {
            return _service.DeleteMarker(MarkerID);
        }
    }

}
