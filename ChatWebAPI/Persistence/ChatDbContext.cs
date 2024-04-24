using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ChatWebAPI.Persistence
{
    public class ChatDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>().OwnsOne(c => c.ChatLLMParameters);
            modelBuilder.Entity<Chat>().OwnsOne(c => c.GenerationError);
        }
    }
}
