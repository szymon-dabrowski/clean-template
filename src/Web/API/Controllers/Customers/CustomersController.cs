using Microsoft.AspNetCore.Mvc;

namespace Clean.API.Controllers.Customer;

[Route("[controller]")]
public class CustomersController : ApiController
{
    [HttpGet]
    public IActionResult ListCustomers()
    {
        return Ok(Array.Empty<string>());
    }
}
