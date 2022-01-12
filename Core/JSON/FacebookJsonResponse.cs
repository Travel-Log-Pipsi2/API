using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Core.JSON
{
    public partial class FacebookJsonResponse
    {
        [JsonProperty("data")]
        public List<Datum> Data { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("place", NullValueHandling = NullValueHandling.Ignore)]
        public Place Place { get; set; }
    }

    public partial class Place
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("zip", NullValueHandling = NullValueHandling.Ignore)]
        public string Zip { get; set; }

        [JsonProperty("street", NullValueHandling = NullValueHandling.Ignore)]
        public string Street { get; set; }
    }

    public partial class Paging
    {
        [JsonProperty("previous")]
        public Uri Previous { get; set; }

        [JsonProperty("next")]
        public Uri Next { get; set; }
    }

}