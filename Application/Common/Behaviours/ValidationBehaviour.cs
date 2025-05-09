using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ValidationException =AGI.Morn.Application.Common.Exceptions.ValidationException;

namespace AGI.Morn.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var Errors = _validators.Select(a => a.Validate(context))
                                        .Where(a => a.Errors.Any()).
                                        SelectMany(a => a.Errors)
                                        .Select(a => new ValidationEroor(
                                            a.ErrorMessage,
                                            a.PropertyName
                                            )).ToList();
                if (Errors.Any())
                    throw new ValidationException(Errors);
            }
            return await next();
        }
    
    }
}
