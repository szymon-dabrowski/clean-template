using Clean.Modules.Crm.Application.Customers.CreateCustomer;
using Clean.Modules.Crm.Application.Customers.DeleteCustomer;
using Clean.Modules.Crm.Application.Customers.Dto;
using Clean.Modules.Crm.Application.Customers.GetCustomer;
using Clean.Modules.Crm.Application.Customers.GetCustomers;
using Clean.Modules.Crm.Application.Customers.UpdateCustomer;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Web.Api.Common.Controllers;
using Clean.Web.Dto.Crm.Customers.Requests;
using Clean.Web.Dto.Crm.Customers.Responses;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.Api.Modules.Crm.Customers;

[Route("[controller]")]
public class CustomersController : ApiController
{
    private readonly ICrmModule crmModule;

    public CustomersController(ICrmModule crmModule)
    {
        this.crmModule = crmModule;
    }

    [HttpGet]
    public async Task<List<CustomerDto>> GetCustomers()
        => await crmModule.ExecuteQuery(new GetCustomersQuery());

    [HttpGet("{customerId}")]
    public async Task<CustomerDto?> GetCustomer(Guid customerId)
        => await crmModule.ExecuteQuery(new GetCustomerQuery(customerId));

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request)
    {
        var result = await crmModule.ExecuteCommand(
            request.Adapt<CreateCustomerCommand>());

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
            request.BuildAdapter()
                .AddParameters(CustomersMappingConfig.CustomerIdParameter, customerId)
                .AdaptToType<UpdateCustomerCommand>());

        return result.Match(
            _ => Ok(),
            errors => Problem(errors));
    }

    [HttpDelete("{customerId}")]
    public async Task DeleteCustomer(Guid customerId)
        => await crmModule.ExecuteCommand(new DeleteCustomerCommand(customerId));
}