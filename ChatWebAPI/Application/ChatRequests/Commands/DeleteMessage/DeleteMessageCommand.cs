using Domain.Core.Entities;
using MediatR;
using Shared.Application.Abstractions.Messaging;
using System.Windows.Input;

namespace ChatWebAPI.Application.ChatRequests.Commands.DeleteMessage
{
    public record DeleteMessageCommand(int UserId, int ChatId, int MessageIndex) : ICommand<Unit>;
}
