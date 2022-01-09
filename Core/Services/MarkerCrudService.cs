using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ServiceResponse> GetTravelsOfMarker(int markerId)
        {
            IEnumerable<Travel> travels = await _markerRepository.GetTravelsOfMarker(markerId);
            return ServiceResponse<IEnumerable<Travel>>.Success(travels);
        }

        public async Task<ServiceResponse> GetMarkersOfUser(Guid UserID)
        {
            var markersFiltered = await _markerRepository.GetMarkersOfUser(UserID);
            return ServiceResponse<IEnumerable<Marker>>.Success(markersFiltered);
        }

        public async Task<ServiceResponse> CreateMarker(MarkerRequest model)
        {
            Marker createdMarker = await _markerRepository.CreateMarker(model);

            return ServiceResponse<Marker>.Success(createdMarker, "Marker was created successfully");
        }

        public async Task<ServiceResponse> CreateTravel(MarkerTravelRequest model)
        {
            MarkerRequest marker = new(model.Name, model.Country, model.Longitude, model.Latitude);
            TravelRequest travel = new(model.Description, model.StartDate, model.EndDate);

            Marker existingMarker = await _markerRepository.FindMarker(marker);

            if (existingMarker == null)
                existingMarker = await _markerRepository.CreateMarker(marker);

            Travel createdTravel = await _markerRepository.CreateTravel(existingMarker.Id, travel);

            return ServiceResponse<Travel>.Success(createdTravel, "Travel was created successfully");
        }

        public async Task<ServiceResponse> UpdateMarker(int MarkerID, MarkerRequest model)
        {
            Marker marker;
            try
            {
                marker = await _markerRepository.UpdateMarker(MarkerID, model);
            }
            catch (UnauthorizedAccessException)
            {
                return ServiceResponse.Error("Unauthorized access");
            }

            if (marker != null)
            {
                return ServiceResponse<Marker>.Success(marker, "Success, Marker updated.");
            }
            else
                return ServiceResponse.Error("Marker not found");
        }

        public async Task<ServiceResponse> UpdateTravel(int TravelID, TravelRequest model)
        {
            Travel travel;
            try
            {
                travel = await _markerRepository.UpdateTravel(TravelID, model);
            }
            catch (UnauthorizedAccessException)
            {
                return ServiceResponse.Error("Unauthorized access");
            }

            if (travel != null)
            {
                return ServiceResponse<Travel>.Success(travel, "Success, Travel updated.");
            }
            else
                return ServiceResponse.Error("Travel not found");
        }

        public async Task<ServiceResponse> DeleteMarker(int MarkerID)
        {
            try
            {
                await _markerRepository.DeleteMarker(MarkerID);
            }
            catch (UnauthorizedAccessException)
            {
                return ServiceResponse.Error("Unauthorized access");
            }
            catch (MissingMemberException)
            {
                return ServiceResponse.Error("Marker does not exist");
            }

            return ServiceResponse.Success("Success, Marker deleted.");
           
        }

        public async Task<ServiceResponse> DeleteTravel(int TravelID)
        {
            Marker marker;

            try
            {
                marker = await _markerRepository.FindMarkerOfTravel(TravelID);
                await _markerRepository.DeleteTravel(TravelID);

                var TravelList = await _markerRepository.GetTravelsOfMarker(marker.Id);

                if (!TravelList.Any())
                {
                    await _markerRepository.DeleteMarker(marker.Id);
                    return ServiceResponse.Success("Success, Travel deleted. Marker has no travels, Marker deleted.");
                }
                else
                    return ServiceResponse.Success("Success, Travel deleted.");
            }
            catch (UnauthorizedAccessException)
            {
                return ServiceResponse.Error("Unauthorized access");
            }
            catch (MissingMemberException)
            {
                return ServiceResponse.Error("Travel does not exist");
            }

            

        }
    }
}
