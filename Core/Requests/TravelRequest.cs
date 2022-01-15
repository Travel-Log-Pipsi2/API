using System;

namespace Core.Requests
{
    public class TravelRequest
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public TravelRequest(string description, DateTime startDate, DateTime endDate)
        {
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
