using Core.Requests;
using Core.Response;
using System.Threading.Tasks;

namespace Core.Interfaces.Authentication
{
    public interface IRegisterService
    {
        public Task<ServiceResponse> Register(RegisterRequest model);
    }
}
