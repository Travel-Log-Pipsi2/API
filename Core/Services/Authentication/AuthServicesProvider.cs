using Core.Interfaces.Authentication;
using Core.Interfaces.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Models.Identity;

namespace Core.Services.Authentication
{
    internal abstract class AuthServicesProvider
    {
        protected readonly UserManager<User> _userManager;
        protected readonly IConfiguration _config;
        protected readonly IJwtGenerator _jwtGenerator;
        protected readonly IEmailService _emailService;

        public AuthServicesProvider
            (
            UserManager<User> userManager,
            IConfiguration config = null,
            IJwtGenerator jwtGenerator = null,
            IEmailService emailService = null
            )
        {
            _userManager = userManager;
            _config = config;
            _jwtGenerator = jwtGenerator;
            _emailService = emailService;
        }
    }
}
