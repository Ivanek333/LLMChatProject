using Domain.Core.Entities;

namespace ChatWebAPI.Application.DTOs
{
    public record ChatShortInfo(int Id, string Title, string Model, MessageDTO LastMessage);
}
