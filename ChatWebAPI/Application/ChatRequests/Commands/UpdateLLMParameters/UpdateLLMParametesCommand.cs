using Domain.Core.Entities;
using MediatR;
using Shared.Application.Abstractions.Messaging;
using System.Windows.Input;

namespace ChatWebAPI.Application.ChatRequests.Commands.UpdateLLMParameters
{
    public record UpdateLLMParametesCommand(int UserId, int ChatId, LLMParameters Parameters) : ICommand<Unit>;
}
