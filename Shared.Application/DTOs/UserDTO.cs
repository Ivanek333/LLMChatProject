using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.DTOs
{
    public record UserDTO (int Id, string Name, LLMParameters DefaultLLMParameters);
}
