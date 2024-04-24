using Domain.Core.Entities;
using MediatR;
using Shared.Application.Abstractions.Messaging;
using System.Windows.Input;

namespace ChatWebAPI.Application.ChatRequests.Commands.CreateChat
{
    public record CreateChatCommand(int UserId, LLMParameters Parameters) : ICommand<int>;
}
