using Clean.Web.API.Common.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.API.Modules.CRM.Controllers;

[Route("[controller]")]
public class CustomersController : ApiController
{
    [HttpGet]
    public IActionResult ListCustomers()
    {
        return Ok(Array.Empty<string>());
    }
}