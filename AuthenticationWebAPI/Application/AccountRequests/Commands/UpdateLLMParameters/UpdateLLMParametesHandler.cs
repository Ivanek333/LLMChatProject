using AuthenticationWebAPI.Application.Abstractions;
using AuthenticationWebAPI.Application.Exceptions;
using AuthenticationWebAPI.Application.UserRequests.Commands.Register;
using Domain.Core.Entities;
using Mapster;
using MediatR;
using Shared.Application.Abstractions.Messaging;
using Shared.Application.DTOs;

namespace AuthenticationWebAPI.Application.UserRequests.Commands.UpdateLLMParameters
{
    public class UpdateLLMParametesHandler(IUserRepository userRepository) : ICommandHandler<UpdateLLMParametesCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateLLMParametesCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserById(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(request.UserId);
            }
            user.DefaultLLMParameters = request.Parameters;
            await userRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
