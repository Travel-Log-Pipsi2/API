using Core.Requests;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMarkerRepository
    {
        public Task<IEnumerable<Marker>> GetMarkers();
        public Task<IEnumerable<Travel>> GetTravelsOfMarker(int markerId);
        public Task<Marker> FindMarker(MarkerRequest model);
        public Task<Marker> FindMarkerOfTravel(int TravelID);
        public Task<IEnumerable<Marker>> GetMarkersOfUser(Guid UserID);
        public Task<Marker> CreateMarker(MarkerRequest model);
        public Task<Travel> CreateTravel(int markerId, TravelRequest model);
        public Task<Marker> UpdateMarker(int MarkerID, MarkerRequest model);
        public Task<Travel> UpdateTravel(int TravelID, TravelRequest model);
        public Task DeleteMarker(int MarkerID);
        public Task DeleteTravel(int TravelID);

    }
}
