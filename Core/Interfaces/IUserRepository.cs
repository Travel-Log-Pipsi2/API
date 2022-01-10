using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable> GetUsers(List<Guid> ids);
    }
}
