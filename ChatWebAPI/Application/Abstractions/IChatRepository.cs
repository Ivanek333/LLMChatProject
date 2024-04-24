using Domain.Core.Entities;
using Domain.Core.Enums;
using MediatR;

namespace ChatWebAPI.Application.Abstractions
{
    public interface IChatRepository
    {
        public Task<List<Chat>> GetChatsByUserId(int userId, CancellationToken cancellationToken = default);
        public Task<Chat?> GetChat(int chatId, CancellationToken cancellationToken = default);
        public void AddChat(Chat chat);
        public void UpdateChat(Chat chat);
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
