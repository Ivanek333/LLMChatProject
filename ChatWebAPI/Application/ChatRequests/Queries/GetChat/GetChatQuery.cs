using ChatWebAPI.Application.DTOs;
using Domain.Core.Entities;
using Shared.Application.Abstractions.Messaging;

namespace ChatWebAPI.Application.ChatRequests.Queries.GetChat
{
    public record GetChatQuery(int UserId, int ChatId) : IQuery<Chat>;
}
