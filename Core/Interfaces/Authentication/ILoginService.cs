using Core.Requests;
using Core.Response;
using System.Threading.Tasks;

namespace Core.Interfaces.Authentication
{
    public interface ILoginService
    {
        public Task<ServiceResponse> Login(LoginRequest model);
    }
}
