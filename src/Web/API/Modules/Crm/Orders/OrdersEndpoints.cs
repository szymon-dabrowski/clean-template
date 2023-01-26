using Clean.Modules.Crm.Application.Orders.CreateOrder;
using Clean.Modules.Crm.Application.Orders.DeleteOrder;
using Clean.Modules.Crm.Application.Orders.GetOrder;
using Clean.Modules.Crm.Application.Orders.GetOrderDetails;
using Clean.Modules.Crm.Application.Orders.GetOrders;
using Clean.Modules.Crm.Application.Orders.GetOrdersDetails;
using Clean.Modules.Crm.Application.Orders.UpdateOrder;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Web.Api.Common.Endpoints;
using Clean.Web.Api.Common.Errors;
using Clean.Web.Dto.Crm.Orders.Requests;
using Clean.Web.Dto.Crm.Orders.Responses;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.Api.Modules.Crm.Orders;

internal class OrdersEndpoints : IEndpointsModule
{
    private const string OrdersRoute = "/orders";

    public void RegisterEndpoints(WebApplication app)
    {
        app.MapGet(OrdersRoute, async (ICrmModule crmModule) =>
        {
            return await crmModule.ExecuteQuery(new GetOrdersQuery());
        });

        app.MapGet($"{OrdersRoute}/{{orderId}}", async (ICrmModule crmModule, Guid orderId) =>
        {
            return await crmModule.ExecuteQuery(new GetOrderQuery(orderId));
        });

        app.MapGet($"{OrdersRoute}/details", async (ICrmModule crmModule, Guid[] orderId) =>
        {
            return await crmModule.ExecuteQuery(new GetOrdersDetailsQuery(orderId));
        });

        app.MapGet($"{OrdersRoute}/detials/{{orderId}}", async (
            ICrmModule crmModule,
            Guid orderId) =>
        {
            return await crmModule.ExecuteQuery(new GetOrderDetailsQuery(orderId));
        });

        app.MapPost(OrdersRoute, async (ICrmModule crmModule, CreateOrderRequest request) =>
        {
            var result = await crmModule.ExecuteCommand(
                request.Adapt<CreateOrderCommand>());

            return result.Match(
                orderId => Results.Ok(new CreateOrderResponse(orderId)),
                errors => errors.AsProblem());
        });

        app.MapPut($"{OrdersRoute}/{{orderId}}", async (
            ICrmModule crmModule,
            Guid orderId,
            UpdateOrderRequest request) =>
        {
            var result = await crmModule.ExecuteCommand(
                request.BuildAdapter()
                    .AddParameters(OrdersMappingConfig.OrderIdParam, orderId)
                    .AdaptToType<UpdateOrderCommand>());

            return result.Match(
                _ => Results.Ok(),
                errors => errors.AsProblem());
        });

        app.MapDelete($"{OrdersRoute}/{{orderId}}", async (ICrmModule crmModule, Guid orderId) =>
        {
            await crmModule.ExecuteCommand(new DeleteOrderCommand(orderId));
        });
    }
}