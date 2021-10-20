using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Storage.Models;
using Storage.Models.Identity;
using System;

namespace Storage.DataAccessLayer
{
    public class ApiDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Test> TestModels { get; set; }
        public override DbSet<User> Users { get; set; }
    }
}
