using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Core.Interfaces.Authentication;

namespace Core.Services.Authentication
{
    public class LoggedUserProvider : ILoggedUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var loggedUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            loggedUserId = "b294e7e4-12fd-4012-7f9a-08d99a159388";
            return loggedUserId != null ? new Guid(loggedUserId) : throw new UnauthorizedAccessException();
        }
    }
}
