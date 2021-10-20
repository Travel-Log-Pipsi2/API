using Core.Requests;
using Core.Response;
using System.Threading.Tasks;

namespace Core.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        public Task<ServiceResponse> Login(LoginRequest model);
        public Task<ServiceResponse> Register(RegisterRequest model);
    }
}
