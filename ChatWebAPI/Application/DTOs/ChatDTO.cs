using Domain.Core.Entities;
using Domain.Core.Enums;

namespace ChatWebAPI.Application.DTOs
{
    public record ChatDTO (int Id, string Title, List<MessageDTO> Messages, 
        LLMParameters ChatLLMParameters, GenerationState GenerationState,
        LLMGenerationErrorData GenerationError
    );
}
