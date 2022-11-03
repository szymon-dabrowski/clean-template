using Clean.Modules.Shared.Common.Errors;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Clean.Modules.Shared.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        this.validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validatonResult = validator != null
            ? await validator.ValidateAsync(request, cancellationToken)
            : null;

        if (validatonResult == null || validatonResult.IsValid)
            return await next();

        return ResponseFrom(validatonResult.Errors);
    }

    private static TResponse ResponseFrom(List<ValidationFailure> validationFailures)
    {
        try
        {
            var errors = validationFailures
                .ConvertAll(x => Error.Validation(
                    code: x.PropertyName,
                    description: x.ErrorMessage));

            return (TResponse)(dynamic)errors;
        }
        catch
        {
            throw new ValidationException(validationFailures);
        }
    }
}
