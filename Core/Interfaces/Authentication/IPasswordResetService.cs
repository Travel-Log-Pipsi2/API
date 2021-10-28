using Core.Requests;
using Core.Response;
using System.Threading.Tasks;

namespace Core.Interfaces.Authentication
{
    public interface IPasswordResetService
    {
        public Task<ServiceResponse> ResetPassword(ResetPasswordRequest model);
        public Task<ServiceResponse> SendResetPasswordEmail(string email);
    }
}
