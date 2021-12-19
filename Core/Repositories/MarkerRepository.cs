using Core.Interfaces;
using Core.Interfaces.Authentication;
using Core.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models;
using Storage.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var markersList = await _context.MarkerModel.Where(m => !m.IsDeleted).ToListAsync();
            return markersList;
        }

        public async Task<IEnumerable<Marker>> GetMarkersOfUser(Guid UserID)
        {
            var markersListFiltered = await _context.MarkerModel.Where(m => !m.IsDeleted).Where(m => m.UserID == UserID).ToListAsync();
            return markersListFiltered;
        }

        public async Task<Marker> CreateMarker(MarkerRequest model)
        {
            Marker markerRequest = new() { };
            markerRequest.Name = model.Name;
            markerRequest.Description = model.Description;
            markerRequest.Longitude = model.Longitude;
            markerRequest.Latitude = model.Latitude;
            markerRequest.Date = model.Date.Date;
                       
            Guid userId;
            try
            {
                userId = _loggedUserProvider.GetUserId();
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
            markerRequest.UserID = userId;
            await Create(markerRequest);
            return markerRequest;
        }

        public async Task<Marker> UpdateMarker(int MarkerID, MarkerRequest model)
        {
            var existingMarker = await _context.MarkerModel.Where(m => !m.IsDeleted).FirstOrDefaultAsync(m => m.Id == MarkerID);
            if (existingMarker != null)
            {
                existingMarker.Name = model.Name;
                existingMarker.Description = model.Description;
                existingMarker.Longitude = model.Longitude;
                existingMarker.Latitude = model.Latitude;
                existingMarker.Date = model.Date.Date;
                
                await Edit(existingMarker);
                return existingMarker;
            }
            else
                return null;
        }

        public async Task<Marker> DeleteMarker(int MarkerID)
        {
            var markerToDelete = await _context.MarkerModel.Where(m => !m.IsDeleted).FirstOrDefaultAsync(m => m.Id == MarkerID);
            if (markerToDelete != null)
            {
                markerToDelete.IsDeleted = true;
                _context.SaveChanges();

                return markerToDelete;
            }
            else
                return null;
        }

    }
}
