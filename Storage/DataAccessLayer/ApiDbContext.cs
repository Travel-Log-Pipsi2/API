using Core.Interfaces.Authentication;
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
        private readonly ILoggedUserProvider _loggedUserProvider;

        public ApiDbContext(DbContextOptions options, ILoggedUserProvider loggedUserProvider) : base(options)
        {
            _loggedUserProvider = loggedUserProvider;
        }

        public DbSet<Test> TestModels { get; set; }
        public DbSet<Marker> MarkerModel { get; set; }
        public DbSet<Travel> TravelModel { get; set; }
        public override DbSet<User> Users { get; set; }
    }
}
