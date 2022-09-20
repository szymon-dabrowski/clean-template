using Microsoft.AspNetCore.Mvc;

namespace Clean.API.Controllers.Coffie;

[Route("[controller]")]
public class CoffeesController : ApiController
{
    [HttpGet]
    public IActionResult ListCoffees()
    {
        return Ok(Array.Empty<string>());
    }
}
