using Core.Interfaces;
using Core.Interfaces.Authentication;
using Core.Response;
using Microsoft.AspNetCore.Identity;
using Storage.Models;
using Storage.Models.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MarkerCrudService : IMarkerCrudService
    {
        readonly IMarkerRepository _markerRepository;
        readonly ILoggedUserProvider _loggedUserProvider;
        readonly UserManager<User> _userManager;

        public MarkerCrudService(IMarkerRepository markerRepository, ILoggedUserProvider loggedUserProvider, UserManager<User> userManager)
        {
            _markerRepository = markerRepository;
            _loggedUserProvider = loggedUserProvider;
            _userManager = userManager;
        }

        public async Task<ServiceResponse> CreateMarker()
        {            
            User user;
            try
            {
                user = await _userManager.FindByIdAsync(_loggedUserProvider.GetUserId().ToString());
            }
            catch (UnauthorizedAccessException)
            {
                return ServiceResponse.Error("User is not logged in");
            }
            Marker createdMarker = await _markerRepository.CreateMarker();
            user.Markers.Add(createdMarker);
            return ServiceResponse<Marker>.Success(createdMarker);            
            
        }

        public async Task<ServiceResponse> GetMarkers()
        {
            IEnumerable<Marker> markers = await _markerRepository.GetMarkers();
            return ServiceResponse<IEnumerable<Marker>>.Success(markers);
        }
    }
}
