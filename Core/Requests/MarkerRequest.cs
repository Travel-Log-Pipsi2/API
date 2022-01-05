using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class MarkerRequest
    {
        public string Name { get; set; }
        public string Country { get; set; }
        [Required]
        public float Longitude { get; set; }
        [Required]
        public float Latitude { get; set; }

        public MarkerRequest(string name, string country, float longitude, float latitude)
        {
            Name = name;
            Country = country;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
