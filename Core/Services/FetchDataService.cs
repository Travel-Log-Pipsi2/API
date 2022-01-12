using Core.Interfaces;
using Core.Response;
using System.Net.Http;
using System.Threading.Tasks;
using Core.JSON;
using Microsoft.Extensions.Configuration;

namespace Core.Services
{
    public class FetchDataService : IFetchDataService
    {
        readonly HttpClient _client;
        readonly IMarkerCrudService _markerSerice;
        readonly IConfiguration _config;

        public FetchDataService(HttpClient client, IMarkerCrudService markerSerice, IConfiguration config)
        {
            _client = client;
            _markerSerice = markerSerice;
            _config = config;
        }

        public async Task<ServiceResponse> Facebook()
        {
            string link = $"https://graph.facebook.com/" + _config["Facebook:UserId"] + "/posts?fields=place,created_time&access_token=" + _config["Facebook:Token"];

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
