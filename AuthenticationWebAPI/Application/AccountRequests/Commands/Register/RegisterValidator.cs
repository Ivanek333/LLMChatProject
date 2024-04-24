using AuthenticationWebAPI.Application.Abstractions;
using AuthenticationWebAPI.Application.UserRequests.Commands.Authenticate;
using FluentValidation;

namespace AuthenticationWebAPI.Application.UserRequests.Commands.Register
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            var forbiddenCharacters = "@#&!?/\\";
            RuleFor(a => a.Request.UserName)
                .NotEmpty().WithMessage("Username must not be empty")
                .Must(p => !p.Any(forbiddenCharacters.Contains))
                .WithMessage($"Username must not contain any of the characters '{forbiddenCharacters}'");
            RuleFor(a => a.Request.Password)
                .NotEmpty().WithMessage("Password must not be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Must(p => p.Any(char.IsDigit) && p.Any(char.IsLower) && p.Any(char.IsUpper))
                    .WithMessage("Password must at least contain a digit, a lowercase and an uppercase letter");
        }
    }
}
