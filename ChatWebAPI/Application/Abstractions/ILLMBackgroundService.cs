using ChatWebAPI.Application.DTOs;
using Domain.Core.Entities;

namespace ChatWebAPI.Application.Abstractions
{
    public interface ILLMBackgroundService
    {
        public List<string> GetModels();
        public bool CanProcessModel(string model);
        public Task ProcessAsync(int chatId, IServiceScope scope, CancellationToken cancellationToken);
    }
}
