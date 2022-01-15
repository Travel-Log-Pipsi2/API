using Core.Interfaces;
using Core.Interfaces.Authentication;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class ConnectionRepository : BaseRepository<Connection>, IConnectionRepository
    {
        readonly ILoggedUserProvider _loggedUserProvider;

        public ConnectionRepository(ApiDbContext context, ILoggedUserProvider loggedUserProvider) : base(context)
        {
            _loggedUserProvider = loggedUserProvider;
        }

        public async Task<Connection> GetConnection()
        {
            var userId = _loggedUserProvider.GetUserId();
            var connectionInfo = await _context.Connections.Where(c => c.UserId == userId).FirstOrDefaultAsync();

            return connectionInfo;
        }

        public async Task<bool> SaveConnection(Connection connection)
        {
            try
            {
                var connectionInfo = await _context.Connections.Where(c => c.UserId == connection.UserId).FirstOrDefaultAsync();
                if (connectionInfo == null)
                    await Create(connection);
                else
                    await Edit(connection);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
