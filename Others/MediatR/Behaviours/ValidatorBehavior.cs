using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using System.Collections.Generic;

namespace CM.Shared.Kernel.Others.MediatR.Behaviours
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>[] _validators;

        public ValidatorBehavior(IValidator<TRequest>[] validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
                throw new Kernel.Application.Exceptions.ValidationException(failures
                    .Select(failure => new KeyValuePair<string, string[]>(failure.PropertyName, new[] { failure.ErrorMessage }))
                    .ToList());

            var response = await next();

            return response;
        }
    }
}
