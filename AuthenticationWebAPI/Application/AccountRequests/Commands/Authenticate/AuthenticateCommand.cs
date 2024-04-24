using Shared.Application.DTOs;
using Shared.Application.Abstractions.Messaging;
using System.Windows.Input;

namespace AuthenticationWebAPI.Application.UserRequests.Commands.Authenticate
{
    public record AuthenticateCommand(AuthenticationRequest Request) : ICommand<AuthenticationResponse>;
}
