using ChattingService.Application.Abstractions;
using ChattingService.Application.DTOs;
using Shared.Application.Abstractions.Messaging;

namespace ChattingService.Application.Chat.Queries.GetChats
{
    public class GetChatsHandler(IChatRepository chatRepository) : IQueryHandler<GetChatsQuery, List<ChatShortInfo>>
    {
        public async Task<List<ChatShortInfo>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
