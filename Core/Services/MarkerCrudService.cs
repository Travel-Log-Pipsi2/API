using Core.Interfaces;
using Core.Interfaces.Authentication;
using Core.Requests;
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

        public MarkerCrudService(IMarkerRepository markerRepository)
        {
            _markerRepository = markerRepository;
        }

        public async Task<ServiceResponse> GetMarkers()
        {
            IEnumerable<Marker> markers = await _markerRepository.GetMarkers();
            return ServiceResponse<IEnumerable<Marker>>.Success(markers);
        }

        public async Task<ServiceResponse> GetMarkersOfUser(Guid UserID)
        {
            IEnumerable<Marker> markersFiltered = await _markerRepository.GetMarkersOfUser(UserID);
            return ServiceResponse<IEnumerable<Marker>>.Success(markersFiltered);
        }

        public async Task<ServiceResponse> CreateMarker(MarkerRequest model)
        {
            Marker createdMarker = await _markerRepository.CreateMarker(model);

            if (createdMarker != null)
            {
                return ServiceResponse<Marker>.Success(createdMarker, "Marker was created successfully");
            }
            else
                return ServiceResponse.Error("Marker was not created");
        }

        public async Task<ServiceResponse> UpdateMarker(int MarkerID, MarkerRequest model)
        {
            Marker marker = await _markerRepository.UpdateMarker(MarkerID, model);

            if (marker != null)
            {
                return ServiceResponse<Marker>.Success(marker, "Success, Marker updated.");
            }
            else
                return ServiceResponse.Error("Marker not found");
        }

        public async Task<ServiceResponse> DeleteMarker(int MarkerID)
        {
            Marker marker = await _markerRepository.DeleteMarker(MarkerID);

            if (marker != null)
            {
                return ServiceResponse<Marker>.Success(marker, "Success, Marker deleted.");
            }
            else
                return ServiceResponse.Error("Marker not found");
        }
    }
}
