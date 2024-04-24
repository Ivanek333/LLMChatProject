using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationWebAPI.Persistence
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().OwnsOne(u => u.DefaultLLMParameters);
        }
    }
}
