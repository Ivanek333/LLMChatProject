using Shared.Application.Exceptions;

namespace ChatWebAPI.Exceptions
{
    public class ChatNotFoundException : NotFoundException
    {
        public ChatNotFoundException(int chatId)
            : base($"Chat with the id {chatId} was not found.") { }
    }
}
