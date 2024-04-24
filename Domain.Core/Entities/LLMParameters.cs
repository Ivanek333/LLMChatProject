using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class LLMParameters
    {
        public string Model { get; set; }
        public double Temperature { get; set; }
        public int MaxTokens { get; set; }

        public LLMParameters()
        {
            Model = "gpt-3.5-turbo";
            Temperature = 0.5;
            MaxTokens = 250;
        }

        public LLMParameters(string model, double temperature, int maxTokens)
        {
            Model = model;
            Temperature = temperature;
            MaxTokens = maxTokens;
        }
    }
}
