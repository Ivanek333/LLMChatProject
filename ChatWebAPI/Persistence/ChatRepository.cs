using ChatWebAPI.Application.Abstractions;
using ChatWebAPI.Exceptions;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Exceptions;
using System.Threading;

namespace ChatWebAPI.Persistence
{
    public class ChatRepository(ChatDbContext chatDb) : IChatRepository
    {
        public Task<Chat?> GetChat(int chatId, CancellationToken cancellationToken = default)
            => chatDb.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == chatId, cancellationToken);

        public Task<List<Chat>> GetChatsByUserId(int userId, CancellationToken cancellationToken = default)
            => chatDb.Chats.Include(c => c.Messages).Where(c => c.UserId == userId).ToListAsync(cancellationToken);

        public void AddChat(Chat chat)
            => chatDb.Chats.Add(chat);

        public void UpdateChat(Chat chat)
            => chatDb.Update(chat);
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => chatDb.SaveChangesAsync(cancellationToken);
    }
}
