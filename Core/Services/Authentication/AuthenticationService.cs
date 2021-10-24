using Core.Interfaces.Authentication;
using Core.Requests;
using Core.Response;
using System.Threading.Tasks;

namespace Core.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService, ILoginService, IRegisterService
    {

        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;

        public AuthenticationService(ILoginService loginService, IRegisterService registerService)
        {
            _loginService = loginService;
            _registerService = registerService;
        }

        public Task<ServiceResponse> Login(LoginRequest model)
        {
            return _loginService.Login(model);
        }

        public Task<ServiceResponse> ConfirmEmail(ConfirmEmailRequest model)
        {
            return _registerService.ConfirmEmail(model);
        }

        public Task<ServiceResponse> Register(RegisterRequest model)
        {
            return _registerService.Register(model);
        }
    }
}
