using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message)
            : base("Not found", message) { }
    }
}
