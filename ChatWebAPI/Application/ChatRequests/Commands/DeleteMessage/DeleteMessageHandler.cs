using ChatWebAPI.Application.Abstractions;
using ChatWebAPI.Application.ChatRequests.Commands.SendMessage;
using ChatWebAPI.Exceptions;
using Domain.Core.Entities;
using MediatR;
using Shared.Application.Abstractions.Messaging;
using Shared.Application.Exceptions;

namespace ChatWebAPI.Application.ChatRequests.Commands.DeleteMessage
{
    public class DeleteMessageHandler(IChatRepository chatRepository) : ICommandHandler<DeleteMessageCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var chat = await chatRepository.GetChat(request.ChatId, cancellationToken);
            if (chat == null)
            {
                throw new ChatNotFoundException(request.ChatId);
            }
            if (chat.UserId != request.UserId)
            {
                throw new UnauthorizedException($"User {request.UserId} not authorized to delete messages in the chat {request.ChatId}");
            }
            if (request.MessageIndex >= chat.Messages.Count)
            {
                throw new ValidationException(new Dictionary<string, string[]>{
                    { "index", ["must be less than collection size"] }
                });
            }
            chat.Messages.RemoveAt(request.MessageIndex);
            await chatRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
