using Microsoft.EntityFrameworkCore;
using Storage.Models;

namespace Storage.DataAccessLayer
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Test> TestModels { get; set; }
    }
}
