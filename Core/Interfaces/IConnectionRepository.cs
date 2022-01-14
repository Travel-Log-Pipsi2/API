using Storage.Models;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IConnectionRepository
    {
        public Task<Connection> GetConnection(Guid userId);
        public Task<bool> SaveConnection(Connection connection);
    }
}
