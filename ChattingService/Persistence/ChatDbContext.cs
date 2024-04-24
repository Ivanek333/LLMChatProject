using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChattingService.Persistence
{
    public class ChatDbContext : DbContext
    {
        DbSet<Chat> Chats { get; set; }
    }
}
