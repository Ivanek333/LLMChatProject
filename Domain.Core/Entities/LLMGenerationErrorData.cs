using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class LLMGenerationErrorData
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public LLMGenerationErrorData()
        {
            Title = string.Empty;
            Message = string.Empty;
        }
        public LLMGenerationErrorData(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}
