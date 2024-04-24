using ChatWebAPI.Application.Abstractions;
using ChatWebAPI.Exceptions;
using Domain.Core.Entities;
using Shared.Application.Abstractions.Messaging;
using Shared.Application.Exceptions;

namespace ChatWebAPI.Application.ChatRequests.Queries.GetChat
{
    public class GetChatHandler(IChatRepository chatRepository) : IQueryHandler<GetChatQuery, Chat>
    {
        public async Task<Chat> Handle(GetChatQuery request, CancellationToken cancellationToken)
        {
            var chat = await chatRepository.GetChat(request.ChatId, cancellationToken);
            if (chat == null)
            {
                throw new ChatNotFoundException(request.ChatId);
            }
            if (chat.UserId != request.UserId)
            {
                throw new UnauthorizedException($"User {request.UserId} have tried to get chat {request.ChatId}");
            }
            return chat;
        }
    }
}
