using Core.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

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
            return loggedUserId != null ? new Guid(loggedUserId) : throw new UnauthorizedAccessException();
        }
    }
}
