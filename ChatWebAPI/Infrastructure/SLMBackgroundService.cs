using ChatWebAPI.Application.Abstractions;
using ChatWebAPI.Exceptions;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Newtonsoft.Json.Linq;
using System.Text;
using ApplicationException = Shared.Application.Exceptions.ApplicationException;

namespace ChatWebAPI.Infrastructure
{
    public class SLMBackgroundService(
        IChatRepository chatRepository, 
        ILogger<SLMBackgroundService> logger)
        : ILLMBackgroundService
    {
        static readonly List<string> models = new List<string> { "slm" };

        public bool CanProcessModel(string model) => models.Contains(model);
        public List<string> GetModels() => models;

        public async Task ProcessAsync(int chatId, IServiceScope scope, CancellationToken cancellationToken)
        {
            Chat? chat = null;
            try
            {
                chat = await chatRepository.GetChat(chatId, cancellationToken);
                if (chat == null)
                {
                    throw new ChatNotFoundException(chatId);
                }
                chat.GenerationState = GenerationState.Generating;
                await chatRepository.SaveChangesAsync(cancellationToken);

                await Task.Delay(new Random().Next(1000, 3000));

                chat.Messages.Add(new Message(
                    string.Join(' ', chat.Messages.Last().Text.Split(' ').Reverse()), 
                    MessageSender.Agent));
                chat.GenerationState = GenerationState.None;
                await chatRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception while processing LLM generation");
                if (chat != null)
                {
                    var errorData = ex switch
                    {
                        ApplicationException ae => new LLMGenerationErrorData(ae.Title, ae.Message),
                        _ => new LLMGenerationErrorData("Internal error", ex.Message)
                    };
                    chat.GenerationError = errorData;
                    chat.GenerationState = GenerationState.Error;
                    await chatRepository.SaveChangesAsync(cancellationToken);
                }
            }
            finally
            {
                logger.LogInformation(
                    "Finished scoped work, provider hash: {hash}.{nl}",
                    scope.ServiceProvider.GetHashCode(), Environment.NewLine);
            }
        }
    }
}
