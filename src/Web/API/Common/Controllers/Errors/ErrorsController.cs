using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.API.Common.Controllers.Errors;

[ApiController]
[Route(Route)]
public class ErrorsController : ControllerBase
{
    public const string Route = "/errors";

    [HttpGet]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem(title: exception?.Message);
    }
}