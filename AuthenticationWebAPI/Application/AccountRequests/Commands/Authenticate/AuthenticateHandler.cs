using JwtAuthenticationManager;
using Shared.Application.DTOs;
using Shared.Application.Abstractions.Messaging;
using Shared.Application.Exceptions;
using AuthenticationWebAPI.Application.Abstractions;
using AuthenticationWebAPI.Application.Exceptions;
using Mapster;

namespace AuthenticationWebAPI.Application.UserRequests.Commands.Authenticate
{
    public class AuthenticateHandler(JwtTokenHandler jwtTokenHandler, IUserRepository userRepository) : ICommandHandler<AuthenticateCommand, AuthenticationResponse>
    {
        public async Task<AuthenticationResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUser(request.Request);
            if (user == null)
            {
                throw new UserNotFoundException(request.Request.UserName);
            }
            var jwt = jwtTokenHandler.GenerateJwtToken(request.Request.Adapt<JwtGenerationRequest>() with {
                Id = user.Id
            });
            return new AuthenticationResponse(user.Adapt<UserDTO>(), jwt);
        }
    }
}
