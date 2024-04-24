using AuthenticationWebAPI.Application.Abstractions;
using Domain.Core.Entities;
using MediatR;
using Shared.Application.Abstractions.Messaging;

namespace AuthenticationWebAPI.Application.UserRequests.Commands.Register
{
    public class RegisterHandler(IUserRepository userRepository) : ICommandHandler<RegisterCommand, Unit>
    {
        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Request.UserName, request.Request.Password);
            userRepository.AddUser(user);
            await userRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
