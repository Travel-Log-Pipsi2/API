using Core.Requests;
using Core.Response;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMarkerCrudService
    {
        public Task<ServiceResponse> GetMarkers();
        public Task<ServiceResponse> GetTravelsOfMarker(int markerId);
        public Task<ServiceResponse> GetMarkersOfUser(Guid UserID);
        public Task<ServiceResponse> CreateMarker(MarkerRequest model);
        public Task<ServiceResponse> CreateTravel(MarkerTravelRequest model);
        public Task<ServiceResponse> UpdateMarker(int MarkerID, MarkerRequest model);
        public Task<ServiceResponse> UpdateTravel(int TravelID, TravelRequest model);
        public Task<ServiceResponse> DeleteMarker(int MarkerID);
        public Task<ServiceResponse> DeleteTravel(int TravelID);

    }
}
