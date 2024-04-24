using FluentValidation;
using Shared.Application.Validators;

namespace ChatWebAPI.Application.ChatRequests.Commands.UpdateLLMParameters
{
    public class UpdateLLMParametersValidator : AbstractValidator<UpdateLLMParametesCommand>
    {
        public UpdateLLMParametersValidator()
        {
            RuleFor(c => c.Parameters).SetValidator(new LLMParametersValidator());
        }
    }
}
