using Core.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    public interface IExternalLoginService
    {
        public ChallengeResult Request(string provider);

        public Task<ServiceResponse> Login();
    }
}
