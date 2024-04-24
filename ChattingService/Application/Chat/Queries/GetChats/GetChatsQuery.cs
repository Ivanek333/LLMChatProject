using ChattingService.Application.DTOs;
using Shared.Application.Abstractions.Messaging;

namespace ChattingService.Application.Chat.Queries.GetChats
{
    public record GetChatsQuery(int UserId, string token) : IQuery<List<ChatShortInfo>>;
}
