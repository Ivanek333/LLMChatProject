using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public string Title { get; set; }
        public ApplicationException(string title, string message) : base(message)
            => Title = title;
    }
}
