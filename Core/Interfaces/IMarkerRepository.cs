using Core.Requests;
using Core.Response;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMarkerRepository
    {
        public Task<IEnumerable<Marker>> GetMarkers();
        public Task<IEnumerable<Travel>> GetTravels(int markerId);
        public Task<Marker> FindMarker(MarkerRequest model);
        public Task<IEnumerable<Marker>> GetMarkersOfUser(Guid UserID);
        public Task<Marker> CreateMarker(MarkerRequest model);
        public Task<Travel> CreateTravel(int markerId, TravelRequest model);
        public Task<Marker> UpdateMarker(int MarkerID, MarkerRequest model);
        public Task DeleteMarker(int MarkerID);

    }
}
