using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class MarkerRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //[Required]
        public float Longitude { get; set; }
        //[Required]
        public float Latitude { get; set; }
        public DateTime Date { get; set; }
    }
}
