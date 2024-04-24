using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; set; }
        public ValidationException(IReadOnlyDictionary<string, string[]> errors)
            : base("Validation Error", "Some validation errors occured")
            => Errors = errors;
    }
}
