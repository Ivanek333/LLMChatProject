using Shared.Application.Abstractions.Messaging;

namespace ChatWebAPI.Application.ChatRequests.Queries.GetModels
{
    public record GetModelsQuery : IQuery<List<string>>;
}
