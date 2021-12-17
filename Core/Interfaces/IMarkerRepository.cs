using Core.Response;
using Storage.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMarkerRepository
    {
        public Task<Marker> CreateMarker();
        public Task<IEnumerable<Marker>> GetMarkers();
        /*public Task<ServiceResponse> GetMarkersOfUser();
        public Task<ServiceResponse> UpdateMarker();
        public Task<ServiceResponse> DeleteMarker();*/

    }
}
