using Core.Interfaces;
using Core.Response;
using System.Net.Http;
using System.Threading.Tasks;
using Core.JSON;
using Microsoft.Extensions.Configuration;
using Core.Interfaces.Authentication;
using Storage.Models;
using System;

namespace Core.Services
{
    public class FetchDataService : IFetchDataService
    {
        readonly HttpClient _client;
        readonly IMarkerCrudService _markerSerice;
        readonly IConnectionRepository _connectionRepository;
        readonly ILoggedUserProvider _loggedUserProvider;
        readonly IConfiguration _config;

        public FetchDataService(HttpClient client, IMarkerCrudService markerSerice, IConnectionRepository connectionRepository, ILoggedUserProvider loggedUserProvider, IConfiguration config)
        {
            _client = client;
            _markerSerice = markerSerice;
            _connectionRepository = connectionRepository;
            _loggedUserProvider = loggedUserProvider;
            _config = config;
        }

        public async Task<ServiceResponse> Connect(string accessToken, string userProviderId)
        {
            var userId = _loggedUserProvider.GetUserId();
            var result = await _connectionRepository.SaveConnection( new Connection() { 
                AccessToken = accessToken, 
                ProvierId = userProviderId,
                UserId = userId,
                lastConnection = DateTime.Now
            });

            if (result)
                return ServiceResponse.Success("Account connected");
            return ServiceResponse.Error("Account not connected");
        }

        public async Task<ServiceResponse> Facebook()
        {
            var userId = _loggedUserProvider.GetUserId();
            var connectionInfo = await _connectionRepository.GetConnection(userId);
            if (connectionInfo == null)
                return ServiceResponse.Error("Account is not connected with facebook");
            string link = "https://graph.facebook.com/" + $"{ connectionInfo.ProvierId }/posts?fields=place,created_time&since={ connectionInfo.lastConnection.Ticks }&access_token={ connectionInfo.AccessToken }";

            while (true)
            {
                var response = await _client.GetAsync(link);
                var result = await response.Content.ReadAsStringAsync();
                var dataList = JsonHelper.ToClass<FacebookJsonResponse>(result);

                if (dataList.Paging == null)
                    break;

                foreach (var data in dataList.Data)
                {
                    if (data.Place != null)
                    {
                        await _markerSerice.CreateTravel(new Requests.MarkerTravelRequest()
                        {
                            Description = data.Place.Name,
                            Name = data.Place.Location.City,
                            Latitude = (float)data.Place.Location.Latitude,
                            Longitude = (float)data.Place.Location.Longitude,
                            Country = data.Place.Location.Country,
                            StartDate = data.CreatedTime,
                            EndDate = data.CreatedTime
                        });
                    }
                }

                link = dataList.Paging.Next.ToString();
            }

            return ServiceResponse.Success("Locations from facebook's posts added");
        }
    }
}
