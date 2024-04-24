using ChatWebAPI.Application.Abstractions;
using ChatWebAPI.Application.DTOs;
using ChatWebAPI.Exceptions;
using Domain.Core.Entities;
using Mapster;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Application.Abstractions.Messaging;
using Shared.Application.Exceptions;
using System.Threading;
using System;
using ApplicationException = Shared.Application.Exceptions.ApplicationException;

namespace ChatWebAPI.Application.ChatRequests.Commands.SendMessage
{
    public class SendMessageHandler(
        IChatRepository chatRepository, 
        ILogger<SendMessageHandler> logger,
        IServiceProvider serviceProvider) 
        : ICommandHandler<SendMessageCommand, Unit>
    {
        public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var chat = await chatRepository.GetChat(request.ChatId, cancellationToken);
            if (chat == null)
            {
                throw new ChatNotFoundException(request.ChatId);
            }
            if (chat.UserId != request.UserId)
            {
                throw new UnauthorizedException($"User {request.UserId} not authorized to send messages to the chat {request.ChatId}");
            }
            chat.Messages.Add(request.message);
            await chatRepository.SaveChangesAsync(cancellationToken);

            StartBackgroundLLMService(chat, cancellationToken);

            return Unit.Value;
        }

        void StartBackgroundLLMService(Chat chat, CancellationToken cancellationToken)
        {
            var scope = serviceProvider.CreateScope();

            logger.LogInformation(
                "Starting scoped work, provider hash: {hash}.",
                scope.ServiceProvider.GetHashCode());

            var service = scope.ServiceProvider.GetServices<ILLMBackgroundService>()
                .FirstOrDefault(service => service.CanProcessModel(chat.ChatLLMParameters.Model));
            if (service == null)
            {
                throw new ApplicationException("LLM model error", $"Can't find LLM handler for {chat.ChatLLMParameters.Model}");
            }
            logger.LogInformation("Running task");
            Task.Run(() => service.ProcessAsync(chat.Id, scope, cancellationToken));
            // might add cancellation by the token, but in that case need to store all the references...
            // ... maybe in a singleton class with a dictionary of {chatId, token}?
        }
    }
}
