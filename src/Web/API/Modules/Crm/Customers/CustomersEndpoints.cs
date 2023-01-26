using Clean.Modules.Crm.Application.Customers.CreateCustomer;
using Clean.Modules.Crm.Application.Customers.DeleteCustomer;
using Clean.Modules.Crm.Application.Customers.GetCustomer;
using Clean.Modules.Crm.Application.Customers.GetCustomers;
using Clean.Modules.Crm.Application.Customers.UpdateCustomer;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Web.Api.Common.Endpoints;
using Clean.Web.Api.Common.Errors;
using Clean.Web.Dto.Crm.Customers.Requests;
using Clean.Web.Dto.Crm.Customers.Responses;
using Mapster;

namespace Clean.Web.Api.Modules.Crm.Customers;

internal class CustomersEndpoints : IEndpointsModule
{
    private const string CustomersRoute = "/customers";

    public void RegisterEndpoints(WebApplication app)
    {
        app.MapGet(CustomersRoute, async (ICrmModule crmModule) =>
        {
            return await crmModule.ExecuteQuery(new GetCustomersQuery());
        });

        app.MapGet($"{CustomersRoute}/{{customerId}}", async (
            ICrmModule crmModule,
            Guid customerId) =>
        {
            return await crmModule.ExecuteQuery(new GetCustomerQuery(customerId));
        });

        app.MapPost(CustomersRoute, async (ICrmModule crmModule, CreateCustomerRequest request) =>
        {
            var result = await crmModule.ExecuteCommand(
                request.Adapt<CreateCustomerCommand>());

            return result.Match(
                customerId => Results.Ok(new CreateCustomerResponse(customerId)),
                errors => errors.AsProblem());
        });

        app.MapPost($"{CustomersRoute}/{{customerId}}", async (
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
        });

        app.MapDelete($"{CustomersRoute}/{{customerId}}", async (
            ICrmModule crmModule,
            Guid customerId) =>
        {
            await crmModule.ExecuteCommand(new DeleteCustomerCommand(customerId));
        });
    }
}