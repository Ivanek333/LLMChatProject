using ChatWebAPI.Application.ChatRequests.Commands.CreateChat;
using FluentValidation;

namespace ChatWebAPI.Application.ChatRequests.Commands.SendMessage
{
    public class SendMessageValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.ChatId).NotEmpty();
        }
    }
}
