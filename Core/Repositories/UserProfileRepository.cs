using Core.DTOs;
using Core.Interfaces;
using Core.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;
using Storage.DataAccessLayer;
using Storage.Models;
using Storage.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    class UserProfileRepository : IUserProfileRepository
    {
        protected readonly ApiDbContext _context;
        readonly IMarkerRepository _markerRepository;

        readonly UserManager<User> _userManager;
        readonly ILoggedUserProvider _loggedUserProvider;

        public UserProfileRepository(ApiDbContext context, IMarkerRepository markerRepository, UserManager<User> userManager, ILoggedUserProvider loggedUserProvider)
        {
            _context = context;
            _markerRepository = markerRepository;

            _userManager = userManager;
            _loggedUserProvider = loggedUserProvider;
        }
        public async Task<UserDTO> GetInfo()
        {
            var userInfo = await GetInfo(_loggedUserProvider.GetUserId());
            return userInfo;
        }

        public async Task<UserDTO> GetInfo(Guid userId)
        {
            UserDTO userInfo = new();

            User user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return null;

            userInfo.Email = user.Email;
            userInfo.Username = user.UserName;
            userInfo.ID = userId;

            return userInfo;
        }

        public async Task<StatsDTO> GetStats()
        {
            var stats = await GetStats(_loggedUserProvider.GetUserId());
            return stats;
        }

        public async Task<StatsDTO> GetStats(Guid userId)
        {
            // calculate stats
            /*- ilość odwiedzonych miejsc    // marker count?
            - ilość odwiedzonych państw      // markers with different country count
            - ilość podróży ogółem           // travel count
            - ilość dni na wyjeździe ogółem  // travels adding up days
            - najczęściej odwiedzane państwo // max amount of country repeats
            - ilość podróży w obecnym roku   // travel count from year
            - ilość odwiedzonych państw w obecnym roku  // 2 but this year
            */
            StatsDTO stats = new();
            List<string> countries = new();
            List<int> countryRepeatAmount = new();
            List<string> countriesThisYear = new();
            int currentYear = DateTime.Now.Year;

            User user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return null;

            IEnumerable<Marker> markersEnumerable = await _markerRepository.GetMarkersOfUser(userId);
            List<Marker> markers = markersEnumerable.ToList();

            stats.MarkerCount = markers.Count; // Ilość miejsc            

            foreach (Marker marker in markers)
            {
                stats.TravelCount += marker.Travels.Count; // Ilość podróż                         

                foreach (Travel travel in marker.Travels)
                {
                    stats.DayTravelCount += (travel.EndDate - travel.StartDate).Days; // Ilość dni w podróży

                    if (!countries.Contains(marker.Country))
                    {
                        countries.Add(marker.Country);
                        countryRepeatAmount.Add(1);
                    }
                    else
                        countryRepeatAmount[countries.IndexOf(marker.Country)]++;

                    if (travel.StartDate.Year == currentYear || travel.EndDate.Year == currentYear)
                    {
                        stats.CurrentYearTravelCount++; // Ilość podróży w tym roku

                        if (!countriesThisYear.Contains(marker.Country))
                        {
                            countriesThisYear.Add(marker.Country);
                            stats.CurrentYearCountryCount++; // Ilość kraji w tym roku
                        }
                    }
                }
            }
            stats.CountryCount = countries.Count; // Ilość odwiedzonych kraji

            if (countries.Count > 0)
            {
                int index = 0;
                for (int i = 0; i < countryRepeatAmount.Count; i++)
                    if (countryRepeatAmount[i] >= countryRepeatAmount[index])
                    {
                        index = i;
                    }

                stats.MostTravelledToCountry = countries[index]; // Najczęściej odwiedzony kraj
                stats.MostTravelledToCountryAmount = countryRepeatAmount[index]; // Ile razy był odwiedzony
            }

            return stats;
        }
    }
}
