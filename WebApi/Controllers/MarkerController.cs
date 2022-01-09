using Core.Interfaces;
using Core.Requests;
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
    public class MarkerController : ControllerBase
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
        [Route("GetTravels/{markerId?}")]
        public Task<ServiceResponse> GetTravels(int markerId)
        {
            return _service.GetTravelsOfMarker(markerId);
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
        [Route("CreateTravel")]
        public Task<ServiceResponse> CreateTravel(MarkerTravelRequest model)
        {
            return _service.CreateTravel(model);           
        }


        [HttpPut()]
        [Route("UpdateMarker")]
        public Task<ServiceResponse> Update(int MarkerID, [FromBody] MarkerRequest model)
        {
            return _service.UpdateMarker(MarkerID, model);
        }

        [HttpPut()]
        [Route("UpdateTravel")]
        public Task<ServiceResponse> UpdateTravel(int TravelID, [FromBody] TravelRequest model)
        {
            return _service.UpdateTravel(TravelID, model);
        }

        [HttpDelete()]
        [Route("DeleteMarker")]
        public Task<ServiceResponse> Delete(int MarkerID)
        {
            return _service.DeleteMarker(MarkerID);
        }

        [HttpDelete()]
        [Route("DeleteTravel")]
        public Task<ServiceResponse> DeleteTravel(int TravelID)
        {
            return _service.DeleteTravel(TravelID);
        }
    }

}
