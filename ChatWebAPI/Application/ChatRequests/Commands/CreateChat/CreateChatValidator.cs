using FluentValidation;

namespace ChatWebAPI.Application.ChatRequests.Commands.CreateChat
{
    public class CreateChatValidator : AbstractValidator<CreateChatCommand>
    {
        public CreateChatValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.Parameters).NotEmpty();
        }
    }
}
