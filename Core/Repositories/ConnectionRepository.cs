using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class ConnectionRepository : BaseRepository<Connection>, IConnectionRepository
    {
        public ConnectionRepository(ApiDbContext context) : base(context) { }

        public async Task<Connection> GetConnection(Guid userId)
        {
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
