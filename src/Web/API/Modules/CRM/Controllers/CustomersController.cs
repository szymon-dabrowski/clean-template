using Clean.Web.Api.Common.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.Api.Modules.Crm.Controllers;

[Route("[controller]")]
public class CustomersController : ApiController
{
    [HttpGet]
    public IActionResult ListCustomers()
    {
        return Ok(Array.Empty<string>());
    }
}