using Clean.Modules.Crm.Dto.Commands.Customers;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Web.Api.Common.Controllers;
using Clean.Web.Dto.Crm.Customers.Requests;
using Clean.Web.Dto.Crm.Customers.Responses;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.Api.Modules.Crm.Customers;

[Route("[controller]")]
public class CustomersController : ApiController
{
    private readonly ICrmModule crmModule;
    private readonly IMapper mapper;

    public CustomersController(ICrmModule crmModule, IMapper mapper)
    {
        this.crmModule = crmModule;
        this.mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request)
    {
        var result = await crmModule.ExecuteCommand(
            mapper.Map<CreateCustomerCommand>(request));

        return result.Match(
            customerId => Ok(new CreateCustomerResponse(customerId)),
            errors => Problem(errors));
    }

    [HttpPut("{customerId}")]
    public async Task<IActionResult> UpdateCustomer(
        Guid customerId,
        UpdateCustomerRequest request)
    {
        var result = await crmModule.ExecuteCommand(
            mapper.From(request)
                .AddParameters(CustomersMappingConfig.CustomerIdParameter, customerId)
                .AdaptToType<UpdateCustomerCommand>());

        return result.Match(
            _ => Ok(),
            errors => Problem(errors));
    }

    [HttpDelete("{customerId}")]
    public async Task DeleteCustomer(Guid customerId)
        => await crmModule.ExecuteCommand(new DeleteCustomerCommand(customerId));

    [HttpGet]
    public IActionResult ListCustomers()
    {
        return Ok(Array.Empty<string>());
    }
}