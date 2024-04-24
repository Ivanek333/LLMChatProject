using ChatWebAPI.Application.Abstractions;
using Domain.Core.Entities;
using MediatR;
using Shared.Application.Abstractions.Messaging;

namespace ChatWebAPI.Application.ChatRequests.Commands.CreateChat
{
    public class CreateChatHandler(IChatRepository chatRepository) : ICommandHandler<CreateChatCommand, int>
    {
        public async Task<int> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = new Chat();
            chat.UserId = request.UserId;
            chat.ChatLLMParameters = request.Parameters;

            chatRepository.AddChat(chat);
            await chatRepository.SaveChangesAsync(cancellationToken);

            return chat.Id;
        }
    }
}
