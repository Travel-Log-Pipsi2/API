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
        [Route("UpdateMarker/{markerId?}")]
        public Task<ServiceResponse> Update(int markerId, [FromBody] MarkerRequest model)
        {
            return _service.UpdateMarker(markerId, model);
        }

        [HttpPut()]
        [Route("UpdateTravel/{travelId?}")]
        public Task<ServiceResponse> UpdateTravel(int travelId, [FromBody] TravelRequest model)
        {
            return _service.UpdateTravel(travelId, model);
        }

        [HttpDelete()]
        [Route("DeleteMarker/{markerId?}")]
        public Task<ServiceResponse> Delete(int markerId)
        {
            return _service.DeleteMarker(markerId);
        }

        [HttpDelete()]
        [Route("DeleteTravel/{travelId?}")]
        public Task<ServiceResponse> DeleteTravel(int travelId)
        {
            return _service.DeleteTravel(travelId);
        }
    }

}
