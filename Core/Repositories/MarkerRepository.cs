﻿using Core.Interfaces;
using Core.Interfaces.Authentication;
using Core.Requests;
using Core.Response;
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
            var markersList = await _context.MarkerModel.ToListAsync();
            return markersList;
        }

        public async Task<IEnumerable<Travel>> GetTravels(int markerId)
        {
            var travelList = await _context.TravelModel.Where(m => m.MarkerId == markerId).ToListAsync();
            return travelList;
        }

        public async Task<Marker> FindMarker(MarkerRequest model)
        {
            var marker = await _context.MarkerModel
                .Where(m => m.Longitude == model.Longitude)
                .Where(m => m.Latitude == model.Latitude)
                .FirstOrDefaultAsync();
            return marker;
        }

        public async Task<IEnumerable<Marker>> GetMarkersOfUser(Guid UserID)
        {
            var markersListFiltered = await _context.MarkerModel.Where(m => m.UserID == UserID).Include(m => m.Travels).ToListAsync();
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

    }
}
