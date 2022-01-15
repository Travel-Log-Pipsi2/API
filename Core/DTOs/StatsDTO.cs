namespace Core.DTOs
{
    public class StatsDTO
    {
        public int MarkerCount { get; set; }
        public int CountryCount { get; set; }
        public int TravelCount { get; set; }
        public int DayTravelCount { get; set; }
        public string MostTravelledToCountry { get; set; }
        public int MostTravelledToCountryAmount { get; set; }
        public int CurrentYearTravelCount { get; set; }
        public int CurrentYearCountryCount { get; set; }
    }
}
