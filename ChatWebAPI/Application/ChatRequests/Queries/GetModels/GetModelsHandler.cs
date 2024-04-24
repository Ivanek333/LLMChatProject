using ChatWebAPI.Application.Abstractions;
using ChatWebAPI.Application.ChatRequests.Queries.GetChats;
using ChatWebAPI.Application.DTOs;
using Domain.Core.Enums;
using Shared.Application.Abstractions.Messaging;

namespace ChatWebAPI.Application.ChatRequests.Queries.GetModels
{
    public class GetModelsHandler(IServiceProvider serviceProvider) : IQueryHandler<GetModelsQuery, List<string>>
    {
        public Task<List<string>> Handle(GetModelsQuery request, CancellationToken cancellationToken)
        {
            var models = serviceProvider.GetServices<ILLMBackgroundService>()
                .SelectMany(s => s.GetModels())
                .ToList();
            return Task.FromResult(models);
        }
    }
}
