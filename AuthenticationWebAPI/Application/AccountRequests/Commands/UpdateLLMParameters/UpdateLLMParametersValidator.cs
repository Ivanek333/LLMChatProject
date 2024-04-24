using FluentValidation;
using Shared.Application.Validators;

namespace AuthenticationWebAPI.Application.UserRequests.Commands.UpdateLLMParameters
{
    public class UpdateLLMParametersValidator : AbstractValidator<UpdateLLMParametesCommand>
    {
        public UpdateLLMParametersValidator()
        {
            RuleFor(c => c.Parameters).SetValidator(new LLMParametersValidator());
        }
    }
}
