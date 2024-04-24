using ChatWebAPI.Application.Abstractions;
using ChatWebAPI.Exceptions;
using Domain.Core.Entities;
using Mapster;
using MediatR;
using Shared.Application.Abstractions.Messaging;
using Shared.Application.DTOs;
using Shared.Application.Exceptions;

namespace ChatWebAPI.Application.ChatRequests.Commands.UpdateLLMParameters
{
    public class UpdateLLMParametesHandler(IChatRepository chatRepository) : ICommandHandler<UpdateLLMParametesCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateLLMParametesCommand request, CancellationToken cancellationToken)
        {
            var chat = await chatRepository.GetChat(request.ChatId, cancellationToken);
            if (chat == null)
            {
                throw new ChatNotFoundException(request.ChatId);
            }
            if (chat.UserId != request.UserId)
            {
                throw new UnauthorizedException($"User {request.UserId} not authorized to send messages to the chat {request.ChatId}");
            }
            chat.ChatLLMParameters = request.Parameters;
            await chatRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
