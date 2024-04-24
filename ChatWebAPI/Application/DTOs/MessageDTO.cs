using Domain.Core.Enums;

namespace ChatWebAPI.Application.DTOs
{
    public record MessageDTO (string Text, MessageSender Sender);
}
