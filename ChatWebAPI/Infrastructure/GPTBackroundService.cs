using ChatWebAPI.Application.Abstractions;
using ChatWebAPI.Exceptions;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;
using ApplicationException = Shared.Application.Exceptions.ApplicationException;

namespace ChatWebAPI.Infrastructure
{
    public class GPTBackroundService(
        //HttpClient httpClient,
        IChatRepository chatRepository,
        ILogger<GPTBackroundService> logger)
        : ILLMBackgroundService
    {
        static readonly List<string> models = new List<string> { "gpt-3.5-turbo" };

        public bool CanProcessModel(string model) => models.Contains(model);
        public List<string> GetModels() => models;

        public async Task ProcessAsync(int chatId, IServiceScope scope, CancellationToken cancellationToken)
        {
            Chat? chat = null;
            try
            {
                logger.LogInformation("in task");
                var httpClient = new HttpClient();
                chat = await chatRepository.GetChat(chatId, cancellationToken);
                if (chat == null)
                {
                    throw new ChatNotFoundException(chatId);
                }
                chat.GenerationState = GenerationState.Generating;
                await chatRepository.SaveChangesAsync(cancellationToken);
                var messages = new List<Dictionary<string, string>> {
                    new Dictionary<string, string> { { "role", "system" }, { "content", "" } } 
                };
                foreach (var m in chat.Messages)
                    messages.Add(new Dictionary<string, string> {
                        { "role", m.Sender switch
                        {
                            MessageSender.User => "user",
                            MessageSender.Agent => "assistant",
                            MessageSender.System => "system",
                            _ => "user"
                        } },
                        { "content", m.Text }
                    });
                string payload = JsonSerializer.Serialize(new
                {
                    model = chat.ChatLLMParameters.Model,
                    messages = messages,
                    max_tokens = chat.ChatLLMParameters.MaxTokens,
                    temperature = chat.ChatLLMParameters.Temperature
                });

                logger.LogInformation($"OpenAI API request ({chat.Id}):" + Environment.NewLine + payload);

                var endpoint = Environment.GetEnvironmentVariable("OPENAI_ENDPOINT");
                var openAiToken = Environment.GetEnvironmentVariable("OPENAI_TOKEN");

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + openAiToken);
                var response = await httpClient.PostAsync(endpoint, new StringContent(payload, Encoding.UTF8, "application/json"));
                string responseBody = await response.Content.ReadAsStringAsync();

                logger.LogInformation("OpenAI response:" + Environment.NewLine + responseBody);

                var newMessage = JObject.Parse(responseBody)?["choices"]?[0]?["message"]?["content"]?.ToString();
                if (newMessage == null)
                {
                    throw new ApplicationException("OpenAI API no response error", "Response didn't contain any messages");
                }
                chat.Messages.Add(new Message(newMessage, MessageSender.Agent));
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
