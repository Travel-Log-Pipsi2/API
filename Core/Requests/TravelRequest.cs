using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class TravelRequest
    {
        public MarkerRequest markerRequest { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }      
        public DateTime EndDate { get; set; }
    }
}
