using Storage.Models;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IConnectionRepository
    {
        public Task<Connection> GetConnection();
        public Task<bool> SaveConnection(Connection connection);
    }
}
