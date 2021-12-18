using Core.Requests;
using Core.Response;
using Storage.Models;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMarkerCrudService
    {
        public Task<ServiceResponse> GetMarkers();
        public Task<ServiceResponse> GetMarkersOfUser(Guid UserID);
        public Task<ServiceResponse> CreateMarker(MarkerRequest model);
        public Task<ServiceResponse> UpdateMarker(int MarkerID, MarkerRequest model);
        public Task<ServiceResponse> DeleteMarker(int MarkerID);

    }
}
