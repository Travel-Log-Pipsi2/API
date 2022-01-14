using Core.Response;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFetchDataService
    {
        public Task<ServiceResponse> Facebook();
    }
}
