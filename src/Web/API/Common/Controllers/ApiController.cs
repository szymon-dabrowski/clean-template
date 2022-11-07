using Clean.Modules.Shared.Common.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Clean.Web.API.Common.Controllers;

[Authorize]
[ApiController]
public class ApiController : ControllerBase
{
    public const string HttpContextErrorsItemKey = "errors";

    protected IActionResult Problem(IReadOnlyCollection<Error> errors, int? statusCode = null)
    {
        if (!errors.Any()) return Problem();

        if (errors.All(e => e.Type == ErrorType.Validation)) return ValidationProblem(errors);

        var problemStatusCode = GetProblemStatusCode(statusCode, errors.First());

        return Problem(problemStatusCode, errors);
    }

    private IActionResult ValidationProblem(IReadOnlyCollection<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }

    private IActionResult Problem(int statusCode, IReadOnlyCollection<Error> errors)
    {
        HttpContext.Items[HttpContextErrorsItemKey] = errors;

        return Problem(statusCode: statusCode, title: "One or more application errors has occurred.");
    }

    private int GetProblemStatusCode(int? statusCode, Error firstError) => (statusCode, firstError.Type) switch
    {
        (null, ErrorType.NotFound) => StatusCodes.Status404NotFound,
        (null, ErrorType.Conflict) => StatusCodes.Status409Conflict,
        (null, ErrorType.Validation) => StatusCodes.Status400BadRequest,
        (not null, _) => statusCode.Value,
        _ => StatusCodes.Status500InternalServerError,
    };
}
