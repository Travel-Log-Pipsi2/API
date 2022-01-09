using System;

namespace Core.DTOs
{
    public class StatsDTO
    {
        /*- ilość odwiedzonych miejsc    // marker count?
            - ilość odwiedzonych państw      // markers with different country count
            - ilość podróży ogółem           // travel count
            - ilość dni na wyjeździe ogółem  // travels adding up days
            - najczęściej odwiedzane państwo // max amount of country repeats
            - ilość podróży w obecnym roku   // travel count from year
            - ilość odwiedzonych państw w obecnym roku  // 2 but this year
            */

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
