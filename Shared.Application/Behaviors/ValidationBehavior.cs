using FluentValidation;
using MediatR;
using Shared.Application.Abstractions.Messaging;
using FluentValidation.Results;

namespace Shared.Application.Behaviors
{
    public class ValidationBehavior<TCommand, TResponse>(IEnumerable<IValidator<TCommand>> validators)
        : IPipelineBehavior<TCommand, TResponse>
        where TCommand : class, ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TCommand request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TCommand>(request);

            var validationResults = new List<ValidationResult>();
            foreach (var v in validators)
            {
                var r = await v.ValidateAsync(context, cancellationToken);
                if (r != null)
                    validationResults.Add(r);
            }
            var errors = validationResults
                .SelectMany(r => r.Errors)
                .Where(e => e != null)
                .GroupBy(
                    e => e.PropertyName,
                    e => e.ErrorMessage,
                    (property, error) => new
                    {
                        Key = property,
                        Values = error.Distinct().ToArray()
                    })
                .ToDictionary(e => e.Key, e => e.Values);

            if (errors.Any())
            {
                throw new Exceptions.ValidationException(errors);
            }

            return await next();
        }
    }
}
