using Core.Response;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFetchPostsService
    {
        public Task<ServiceResponse> Facebook();
        public Task<ServiceResponse> Connect(string accessToken, string userProviderId);
    }
}
