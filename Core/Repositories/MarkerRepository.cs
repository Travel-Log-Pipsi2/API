using Core.Interfaces;
using Core.Interfaces.Authentication;
using Core.Requests;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    class MarkerRepository : BaseRepository<Marker>, IMarkerRepository
    {
        readonly ILoggedUserProvider _loggedUserProvider;

        public MarkerRepository(ApiDbContext context, ILoggedUserProvider loggedUserProvider) : base(context) {
            _loggedUserProvider = loggedUserProvider;
        }
        public async Task<IEnumerable<Marker>> GetMarkers()
        {
            var markersList = await _context.MarkerModel.ToListAsync();
            return markersList;
        }

        public async Task<IEnumerable<Travel>> GetTravelsOfMarker(int markerId)
        {
            var travelList = await _context.TravelModel.Where(m => m.MarkerId == markerId).ToListAsync();
            return travelList;
        }

        public async Task<Marker> FindMarker(MarkerRequest model)
        {
            var marker = await _context.MarkerModel
                .Where(m => m.UserID == _loggedUserProvider.GetUserId())
                .Where(m => m.Longitude == model.Longitude)
                .Where(m => m.Latitude == model.Latitude)
                .FirstOrDefaultAsync();
            return marker;
        }

        public async Task<Marker> FindMarkerOfTravel(int TravelID)
        {
            var travel = await _context.TravelModel.FirstOrDefaultAsync(t => t.Id == TravelID);

            if (travel == null)
                throw new MissingMemberException();

            var marker = await _context.MarkerModel.FirstOrDefaultAsync(m => m.Id == travel.MarkerId);

            if (marker.UserID != _loggedUserProvider.GetUserId())
                throw new UnauthorizedAccessException();            

            return marker;
        }

        public async Task<IEnumerable<Marker>> GetMarkersOfUser(Guid UserID)
        {
            var markersListFiltered = await _context.MarkerModel.Where(m => m.UserID == UserID).Include(m => m.Travels.OrderBy(t => t.StartDate)).ToListAsync();
            return markersListFiltered;
        }

        public async Task<Marker> CreateMarker(MarkerRequest model)
        {
            Marker markerRequest = new() { };
            markerRequest.Name = model.Name;
            markerRequest.Country = model.Country;
            markerRequest.Longitude = model.Longitude;
            markerRequest.Latitude = model.Latitude;
                       
            Guid userId = _loggedUserProvider.GetUserId();
            markerRequest.UserID = userId;
            await Create(markerRequest);
            return markerRequest;
        }

        public async Task<Travel> CreateTravel(int markerId, TravelRequest model)
        {
            Travel travel = new();
            travel.Description = model.Description;
            travel.StartDate = model.StartDate;
            travel.EndDate = model.EndDate;
            travel.MarkerId = markerId;

            _context.TravelModel.Add(travel);
            await _context.SaveChangesAsync();

            return travel;
        }

        public async Task<Marker> UpdateMarker(int MarkerID, MarkerRequest model)
        {
            var existingMarker = await _context.MarkerModel.FirstOrDefaultAsync(m => m.Id == MarkerID);
            if (existingMarker != null)
            {
                if (existingMarker.UserID != _loggedUserProvider.GetUserId())
                    throw new UnauthorizedAccessException();
                existingMarker.Name = model.Name;
                existingMarker.Country = model.Country;
                existingMarker.Longitude = model.Longitude;
                existingMarker.Latitude = model.Latitude;

                await Edit(existingMarker);
                return existingMarker;
            }
            else
                return null;
        }

        public async Task<Travel> UpdateTravel(int TravelID, TravelRequest model)
        {
            var existingTravel = await _context.TravelModel.FirstOrDefaultAsync(t => t.Id == TravelID);
            if (existingTravel != null)
            {
                var marker = await _context.MarkerModel.FirstOrDefaultAsync(m => m.Id == existingTravel.MarkerId);
                if (marker.UserID != _loggedUserProvider.GetUserId())
                    throw new UnauthorizedAccessException();
                
                existingTravel.Description = model.Description;
                existingTravel.StartDate = model.StartDate;
                existingTravel.EndDate = model.EndDate;

                _context.TravelModel.Update(existingTravel);
                await _context.SaveChangesAsync();

                return existingTravel;
            }
            else
                return null;
        }

        public async Task DeleteMarker(int MarkerID)
        {
            var markerToDelete = await _context.MarkerModel.FirstOrDefaultAsync(m => m.Id == MarkerID);
            if (markerToDelete != null)
            {
                if (markerToDelete.UserID != _loggedUserProvider.GetUserId())
                    throw new UnauthorizedAccessException();

                await Delete(markerToDelete);

                return;
            }
            else
                throw new MissingMemberException();
        }

        public async Task DeleteTravel(int TravelID)
        {
            var travelToDelete = await _context.TravelModel.FirstOrDefaultAsync(t => t.Id == TravelID);            

            if (travelToDelete != null)
            {
                var marker = await _context.MarkerModel.FirstOrDefaultAsync(m => m.Id == travelToDelete.MarkerId);
                if (marker.UserID != _loggedUserProvider.GetUserId())
                    throw new UnauthorizedAccessException();
                
                _context.TravelModel.Remove(travelToDelete);
                await _context.SaveChangesAsync();

                return;
            }
            else
                throw new MissingMemberException();
        }

    }
}
