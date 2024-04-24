using Domain.Core.Entities;
using MediatR;
using Shared.Application.Abstractions.Messaging;

namespace ChatWebAPI.Application.ChatRequests.Commands.SendMessage
{
    public record SendMessageCommand(int UserId, int ChatId, Message message) : ICommand<Unit>;
}
