using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    class MarkerRepository : BaseRepository<Marker>, IMarkerRepository
    {

        public MarkerRepository(ApiDbContext context) : base(context) { }

        public async Task<Marker> CreateMarker()
        {
            Marker markerRequest = new() { };
            await Create(markerRequest);
            return markerRequest;
        }
        public async Task<IEnumerable<Marker>> GetMarkers()
        {
            var markersList = await _context.MarkerModel.ToListAsync();
            return markersList;
        }

    }
}
