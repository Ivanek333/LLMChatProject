using Domain.Core.Entities;
using MediatR;
using Shared.Application.Abstractions.Messaging;
using System.Windows.Input;

namespace AuthenticationWebAPI.Application.UserRequests.Commands.UpdateLLMParameters
{
    public record UpdateLLMParametesCommand(int UserId, LLMParameters Parameters) : ICommand<Unit>;
}
