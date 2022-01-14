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
    public class FetchPostsService : IFetchPostsService
    {
        readonly HttpClient _client;
        readonly IMarkerCrudService _markerSerice;
        readonly IConnectionRepository _connectionRepository;
        readonly ILoggedUserProvider _loggedUserProvider;

        public FetchPostsService(HttpClient client, IMarkerCrudService markerSerice, IConnectionRepository connectionRepository, ILoggedUserProvider loggedUserProvider)
        {
            _client = client;
            _markerSerice = markerSerice;
            _connectionRepository = connectionRepository;
            _loggedUserProvider = loggedUserProvider;
        }

        public async Task<ServiceResponse> Connect(string accessToken, string userProviderId)
        {
            var userId = _loggedUserProvider.GetUserId();
            var connection = await _connectionRepository.GetConnection();
            if (connection != null)
            {
                connection.GenerationTime = DateTime.Now;
                connection.AccessToken = accessToken;
                await _connectionRepository.SaveConnection(connection);

                return ServiceResponse.Success("Account connection refreshed");
            }
               
            var result = await _connectionRepository.SaveConnection( new Connection() { 
                AccessToken = accessToken, 
                ProvierId = userProviderId,
                UserId = userId,
                GenerationTime = DateTime.Now
            });

            if (result)
                return ServiceResponse.Success("Account connected");
            return ServiceResponse.Error("Error occured while connecting account");
        }

        public async Task<ServiceResponse> Facebook()
        {
            var connectionInfo = await _connectionRepository.GetConnection();
            if (connectionInfo == null || (DateTime.Now - connectionInfo.GenerationTime).Hours > 2)
                return ServiceResponse.Error("Account is not connected with facebook. Please refresh connection");

            string link = "https://graph.facebook.com/" + $"{ connectionInfo.ProvierId }/posts?fields=place,created_time&since={ connectionInfo.LastConnectionTime.Ticks }&access_token={ connectionInfo.AccessToken }";

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
            connectionInfo.LastConnectionTime = DateTime.Now;
            await _connectionRepository.SaveConnection(connectionInfo);

            return ServiceResponse.Success("Locations from facebook's posts added");
        }
    }
}
