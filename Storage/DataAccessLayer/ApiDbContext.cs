using Microsoft.EntityFrameworkCore;
using Storage.Models;
using Storage.Models.Identity;

namespace Storage.DataAccessLayer
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Test> TestModels { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
