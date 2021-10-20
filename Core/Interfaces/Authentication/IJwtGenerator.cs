using Microsoft.Extensions.Configuration;
using Storage.Models.Identity;
using System.Collections.Generic;

namespace Core.Interfaces.Authentication
{
    public interface IJwtGenerator
    {
        public string GenerateJWTToken(IConfiguration _config, User user, IList<string> roles);
    }
}
