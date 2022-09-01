using Clean.Common.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Clean.API.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    public const string HttpContextErrorCodeITemKey = "errorCode";

    protected IActionResult Problem(int statusCode, IError error)
    {
        HttpContext.Items[HttpContextErrorCodeITemKey] = error.Code;

        return Problem(statusCode: statusCode, title: error.Description);
    }
}
