using Clean.Modules.Shared.Common.Errors;

namespace Clean.Web.Api.Common.Errors;

internal static class ErrorsExtensions
{
    public static IResult AsProblem(this IReadOnlyCollection<Error> errors, int? statusCode = null)
    {
        if (!errors.Any())
        {
            return Results.Problem();
        }

        if (errors.All(e => e.Type == ErrorType.Validation))
        {
            return errors.AsValidationProblem();
        }

        var problemStatusCode = GetProblemStatusCode(statusCode, errors.First());

        return Problem(problemStatusCode, errors);
    }

    private static IResult AsValidationProblem(this IReadOnlyCollection<Error> errors)
    {
        var test = errors
            .GroupBy(e => e.Code)
            .ToDictionary(
                e => e.Key,
                e => e.Select(ee => ee.Description).ToArray());

        return Results.ValidationProblem(test);
    }

    private static int GetProblemStatusCode(int? statusCode, Error firstError) => (statusCode, firstError.Type) switch
    {
        (null, ErrorType.NotFound) => StatusCodes.Status404NotFound,
        (null, ErrorType.Conflict) => StatusCodes.Status409Conflict,
        (null, ErrorType.Validation) => StatusCodes.Status400BadRequest,
        (not null, _) => statusCode.Value,
        _ => StatusCodes.Status500InternalServerError,
    };

    private static IResult Problem(int statusCode, IReadOnlyCollection<Error> errors)
    {
        var extensions = new Dictionary<string, object?>()
        {
            { "errors", errors },
        };

        return Results.Problem(
            statusCode: statusCode,
            title: "One or more application errors has occurred.",
            extensions: extensions);
    }
}