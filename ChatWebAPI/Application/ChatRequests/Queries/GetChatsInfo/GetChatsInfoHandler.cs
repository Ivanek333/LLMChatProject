using ChatWebAPI.Application.Abstractions;
using ChatWebAPI.Application.DTOs;
using Domain.Core.Enums;
using Mapster;
using Shared.Application.Abstractions.Messaging;

namespace ChatWebAPI.Application.ChatRequests.Queries.GetChats
{
    public class GetChatsInfoHandler(IChatRepository chatRepository) : IQueryHandler<GetChatsInfoQuery, List<ChatShortInfo>>
    {
        public async Task<List<ChatShortInfo>> Handle(GetChatsInfoQuery request, CancellationToken cancellationToken)
        {
            var chats = await chatRepository.GetChatsByUserId(request.UserId, cancellationToken);
            return chats.Select(c => new ChatShortInfo(
                c.Id, c.Title, c.ChatLLMParameters.Model, 
                c.Messages.Count > 0 ? c.Messages.Last().Adapt<MessageDTO>() : new MessageDTO("New chat", MessageSender.System)
            )).ToList();
        }
    }
}
