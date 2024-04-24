using ChatWebAPI.Application.DTOs;
using Shared.Application.Abstractions.Messaging;

namespace ChatWebAPI.Application.ChatRequests.Queries.GetChats
{
    public record GetChatsInfoQuery(int UserId) : IQuery<List<ChatShortInfo>>;
}
