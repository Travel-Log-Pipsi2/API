using Core.Response;
using Storage.Models;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMarkerCrudService
    {
        public Task<ServiceResponse> CreateMarker();
        public Task<ServiceResponse> GetMarkers();
        /*public Task<ServiceResponse> GetMarkersOfUser();
        public Task<ServiceResponse> UpdateMarker();
        public Task<ServiceResponse> DeleteMarker();*/

    }
}
