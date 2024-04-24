using Domain.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Validators
{
    public class LLMParametersValidator : AbstractValidator<LLMParameters>
    {
        public LLMParametersValidator() 
        {
            RuleFor(p => p.MaxTokens).GreaterThan(0).WithMessage("Max tokens can't be less than 0");
            RuleFor(p => p.Temperature).InclusiveBetween(0, 1).WithMessage("Temperature range is [0, 1]");
        }
    }
}
