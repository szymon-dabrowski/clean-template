using Clean.Modules.Crm.Application.Customers.CreateCustomer;
using Clean.Modules.Crm.Application.Customers.DeleteCustomer;
using Clean.Modules.Crm.Application.Customers.GetCustomer;
using Clean.Modules.Crm.Application.Customers.GetCustomers;
using Clean.Modules.Crm.Application.Customers.UpdateCustomer;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Web.Api.Common.Endpoints;
using Clean.Web.Api.Common.Errors;
using Clean.Web.Api.Common.Permissions;
using Clean.Web.Dto.Crm.Customers.Model;
using Clean.Web.Dto.Crm.Customers.Requests;
using Clean.Web.Dto.Crm.Customers.Responses;
using Mapster;

namespace Clean.Web.Api.Modules.Crm.Customers;

internal class CustomersEndpoints : IEndpointsModule
{
    private const string CustomersRoute = "/customers";

    public void RegisterEndpoints(WebApplication app)
    {
        var customersEndpoints = app.MapGroup(CustomersRoute);

        customersEndpoints.MapGet("/", async (ICrmModule crmModule) =>
        {
            var customers = await crmModule.ExecuteQuery(new GetCustomersQuery());

            return new GetCustomersResponse(customers
                .Select(c => c.Adapt<CustomerDto>())
                .ToList());
        })
            .RequirePermission(CustomersPermissions.Read);

        customersEndpoints.MapGet("/{customerId}", async (
            ICrmModule crmModule,
            Guid customerId) =>
        {
            var customer = await crmModule.ExecuteQuery(new GetCustomerQuery(customerId));

            return new GetCustomerResponse(customer?.Adapt<CustomerDto>());
        })
            .RequirePermission(CustomersPermissions.Read);

        customersEndpoints.MapPost("/", async (ICrmModule crmModule, CreateCustomerRequest request) =>
        {
            var result = await crmModule.ExecuteCommand(
                request.Adapt<CreateCustomerCommand>());

            return result.Match(
                customerId => Results.Ok(new CreateCustomerResponse(customerId)),
                errors => errors.AsProblem());
        })
            .RequirePermission(CustomersPermissions.Write);

        customersEndpoints.MapPost("/{customerId}", async (
            ICrmModule crmModule,
            Guid customerId,
            UpdateCustomerRequest request) =>
        {
            var result = await crmModule.ExecuteCommand(
                request.BuildAdapter()
                    .AddParameters(CustomersMappingConfig.CustomerIdParameter, customerId)
                    .AdaptToType<UpdateCustomerCommand>());

            return result.Match(
                _ => Results.Ok(),
                errors => errors.AsProblem());
        })
            .RequirePermission(CustomersPermissions.Write);

        customersEndpoints.MapDelete("/{customerId}", async (
            ICrmModule crmModule,
            Guid customerId) =>
        {
            await crmModule.ExecuteCommand(new DeleteCustomerCommand(customerId));
        })
            .RequirePermission(CustomersPermissions.Write);
    }
}