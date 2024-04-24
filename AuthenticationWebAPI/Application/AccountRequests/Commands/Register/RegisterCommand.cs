using MediatR;
using Shared.Application.Abstractions.Messaging;
using Shared.Application.DTOs;
using System.Windows.Input;

namespace AuthenticationWebAPI.Application.UserRequests.Commands.Register
{
    public record RegisterCommand(AuthenticationRequest Request) : ICommand<Unit>; 
}
