using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class MarkerTravelRequest
    {
        public string Name { get; set; }
        public string Country { get; set; }
        [Required]
        public float Longitude { get; set; }
        [Required]
        public float Latitude { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
