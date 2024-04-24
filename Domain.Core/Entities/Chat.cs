using Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public List<Message> Messages { get; set; }
        public LLMParameters ChatLLMParameters { get; set; }
        public GenerationState GenerationState { get; set; }
        public LLMGenerationErrorData GenerationError { get; set; }

        public Chat()
        {
            Title = "New chat";
            Messages = new List<Message>();
            ChatLLMParameters = new LLMParameters();
            GenerationState = GenerationState.None;
            GenerationError = new LLMGenerationErrorData();
        }
        public Chat(int userId, LLMParameters parameters) : this()
        {
            UserId = userId;
            ChatLLMParameters = parameters;
        }
    }
}
