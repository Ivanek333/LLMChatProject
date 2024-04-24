using AuthenticationWebAPI.Application.Abstractions;
using FluentValidation;

namespace AuthenticationWebAPI.Application.UserRequests.Commands.Authenticate
{
    public class AuthenticateValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateValidator(IUserRepository userRepository)
        {
            RuleFor(a => a.Request).NotEmpty().MustAsync(userRepository.ValidateUser)
                .WithMessage("Invalid username or password");
        }
    }
}
