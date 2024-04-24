using MediatR;
using Shared.Application.Abstractions.Messaging;

namespace ChattingService.Application.Chat.Commands.SendMessage
{
    public record SendMessageCommand(int UserId, int ChatId, string token, string message) : ICommand<Unit>;
}
