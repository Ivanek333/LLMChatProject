using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Abstractions.Messaging
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>;
}
