using MediatR;
using Shared.Application.Abstractions.Messaging;

namespace ChattingService.Application.Chat.Commands.SendMessage
{
    public class SendMessageHandler : ICommandHandler<SendMessageCommand, Unit>
    {
        public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            // add message and run background scoped LLM service
            throw new NotImplementedException();
        }
    }
}
