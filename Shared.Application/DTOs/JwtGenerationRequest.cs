using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.DTOs
{
    public record JwtGenerationRequest(int Id, string UserName, string Password);
}
