using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Clean.API.Controllers.Error;

[ApiController]
[Route("[controller]")]
public class ErrorsController : ControllerBase
{
    [HttpGet]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem(title: exception?.Message);
    }
}
