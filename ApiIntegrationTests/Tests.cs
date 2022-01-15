using Core.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Storage.Models.Identity;
using System;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApi;
using Xunit;

namespace ApiIntegrationTests
{
    public class Tests : IntegrationTests
    {

        [Fact]
        public async Task Marker_GetMarkers_Id_ShouldBeEmpty()
        {
            await AuthenticateClientAsync();

            var response = await testClient.GetAsync("api/Marker/GetMarkers/" + user.Id);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            json["content"].Should().BeEmpty();
        }

        [Fact]
        public async Task Travel_CreateOneTravel()
        {
            await AuthenticateClientAsync();

            await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName",
                Country = "testCountry1",                
                Latitude = 10,
                Longitude = 5,
                Description = "Test description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            });

            var response = await testClient.GetAsync("api/Marker/GetMarkers/" + user.Id);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            json["content"].Should().HaveCount(1);
            json["content"][0]["name"].ToString().Should().BeEquivalentTo("testName");
            json["content"][0]["travels"].Should().HaveCount(1);
            json["content"][0]["travels"][0]["description"].ToString().Should().BeEquivalentTo("Test description");
            
            ClearInMemoryDatabase();
        }

        [Fact]
        public async Task Travel_CreateTwoTravels()
        {
            await AuthenticateClientAsync();

            await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName",
                Country = "testCountry1",
                Latitude = 10,
                Longitude = 5,
                Description = "1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            });

            await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName",
                Country = "testCountry1",
                Latitude = 10,
                Longitude = 5,
                Description = "2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            });

            var response = await testClient.GetAsync("api/Marker/GetMarkers/" + user.Id);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            json["content"].Should().HaveCount(1);
            json["content"][0]["travels"].Should().HaveCount(2);
            json["content"][0]["travels"][1]["description"].ToString().Should().BeEquivalentTo("2");

            ClearInMemoryDatabase();
        }

        [Fact]
        public async Task Travel_UpdateTravel()
        {
            await AuthenticateClientAsync();

            var createResponse = await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName",
                Country = "testCountry1",
                Latitude = 10,
                Longitude = 5,
                Description = "Before",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            });

            JObject createJson = JObject.Parse(createResponse.Content.ReadAsStringAsync().Result);

            var r = await testClient.PutAsJsonAsync("api/Marker/UpdateTravel/" + (int)createJson["content"]["id"], new TravelRequest
            (
                "After",
                DateTime.Now,
                DateTime.Now
            ));

            var response = await testClient.GetAsync("api/Marker/GetMarkers/" + user.Id);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            json["content"].Should().HaveCount(1);
            json["content"][0]["travels"].Should().HaveCount(1);
            json["content"][0]["travels"][0]["description"].ToString().Should().BeEquivalentTo("After");            

            ClearInMemoryDatabase();
        }

        [Fact]
        public async Task Travel_DeleteOneWithMarker()
        {
            await AuthenticateClientAsync();

            var createResponse = await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName",
                Country = "testCountry1",
                Latitude = 10,
                Longitude = 5,
                Description = "Test description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            });

            JObject createJson = JObject.Parse(createResponse.Content.ReadAsStringAsync().Result);

            await testClient.DeleteAsync("api/Marker/DeleteTravel/" + (int)createJson["content"]["id"]);

            var response = await testClient.GetAsync("api/Marker/GetMarkers/" + user.Id);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            json["content"].Should().HaveCount(0);

            ClearInMemoryDatabase();
        }

        
        [Fact]
        public async Task Travel_DeleteWithTwoTravels()
        {
            await AuthenticateClientAsync();

            await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName",
                Country = "testCountry1",
                Latitude = 10,
                Longitude = 5,
                Description = "1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            });

            var createResponse = await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName",
                Country = "testCountry1",
                Latitude = 10,
                Longitude = 5,
                Description = "2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            });

            JObject createJson = JObject.Parse(createResponse.Content.ReadAsStringAsync().Result);

            await testClient.DeleteAsync("api/Marker/DeleteTravel/" + (int)createJson["content"]["id"]);

            var response = await testClient.GetAsync("api/Marker/GetMarkers/" + user.Id);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            json["content"].Should().HaveCount(1);
            json["content"][0]["travels"].Should().HaveCount(1);

            ClearInMemoryDatabase();
        }
        

        [Fact]
        public async Task Marker_CreateDifferentMarkers()
        {
            await AuthenticateClientAsync();

            await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName",
                Country = "testCountry1",
                Latitude = 10,
                Longitude = 5,
                Description = "1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            });

            await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName2",
                Country = "testCountry2",
                Latitude = 10,
                Longitude = 6,
                Description = "2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            });

            var response = await testClient.GetAsync("api/Marker/GetMarkers/" + user.Id);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            json["content"].Should().HaveCount(2);
            json["content"][0]["travels"].Should().HaveCount(1);
            json["content"][1]["travels"].Should().HaveCount(1);
            json["content"][0]["travels"][0]["description"].ToString().Should().BeEquivalentTo("1");
            json["content"][1]["travels"][0]["description"].ToString().Should().BeEquivalentTo("2");

            ClearInMemoryDatabase();
        }

        [Fact]
        public async Task Info_GetInfoTest()
        {
            await AuthenticateClientAsync();

            var response = await testClient.GetAsync("api/UserInfo/GetInfo");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            json["content"]["id"].ToString().Should().BeEquivalentTo(user.Id.ToString());
        }

        [Fact]
        public async Task Info_GetStatsTest()
        {
            await AuthenticateClientAsync();

            var date = DateTime.Parse("04/04/" + DateTime.Now.Year);

            await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName",
                Country = "testCountry1",
                Latitude = 10,
                Longitude = 5,
                Description = "Test description",
                StartDate = date,
                EndDate = date
            });

            await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName",
                Country = "testCountry1",
                Latitude = 10,
                Longitude = 5,
                Description = "Test description",
                StartDate = date.AddDays(-2),
                EndDate = date
            });

            await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName",
                Country = "testCountry1",
                Latitude = 10,
                Longitude = 5,
                Description = "Test description",
                StartDate = date.AddDays(-20),
                EndDate = date.AddDays(5)
            });

            await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName2",
                Country = "testCountry1",
                Latitude = 10,
                Longitude = 12,
                Description = "Test description",
                StartDate = date,
                EndDate = date
            });

            await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName2",
                Country = "testCountry2",
                Latitude = 10,
                Longitude = 10,
                Description = "Test description",
                StartDate = date,
                EndDate = date
            });

            await testClient.PostAsJsonAsync("api/Marker/CreateTravel", new MarkerTravelRequest
            {
                Name = "testName2",
                Country = "testCountry2",
                Latitude = 10,
                Longitude = 10,
                Description = "Test description",
                StartDate = date.AddYears(-2),
                EndDate = date.AddYears(-1)
            });




            var response = await testClient.GetAsync("api/UserInfo/GetStats");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            ((int)json["content"]["markerCount"]).Should().Be(3);
            ((int)json["content"]["countryCount"]).Should().Be(2);
            ((int)json["content"]["travelCount"]).Should().Be(6);
            ((int)json["content"]["dayTravelCount"]).Should().Be(392);
            json["content"]["mostTravelledToCountry"].ToString().Should().BeEquivalentTo("testCountry1");
            ((int)json["content"]["mostTravelledToCountryAmount"]).Should().Be(4);
            ((int)json["content"]["currentYearTravelCount"]).Should().Be(5);
            ((int)json["content"]["currentYearCountryCount"]).Should().Be(2);


            ClearInMemoryDatabase();
        }
    }
}
