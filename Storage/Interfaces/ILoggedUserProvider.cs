using System;

namespace Core.Interfaces.Authentication
{
    public interface ILoggedUserProvider
    {
        public Guid GetUserId();
    }
}
