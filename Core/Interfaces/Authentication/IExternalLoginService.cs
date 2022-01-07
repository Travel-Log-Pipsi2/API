using Core.Requests;
using Core.Response;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    public interface IExternalLoginService
    {
        public Task<ServiceResponse> Login(FacebookAuthRequest request);
    }
}
