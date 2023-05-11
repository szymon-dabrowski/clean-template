using Clean.Modules.Crm.Application.Orders.CancelOrder;
using Clean.Modules.Crm.Application.Orders.CompleteOrder;
using Clean.Modules.Crm.Application.Orders.ConfirmOrder;
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
using Clean.Web.Api.Common.Permissions;
using Clean.Web.Dto.Crm.Orders.Model;
using Clean.Web.Dto.Crm.Orders.Requests;
using Clean.Web.Dto.Crm.Orders.Responses;
using Mapster;

namespace Clean.Web.Api.Modules.Crm.Orders;

internal class OrdersEndpoints : IEndpointsModule
{
    private const string OrdersRoute = "/orders";

    public void RegisterEndpoints(WebApplication app)
    {
        var ordersEndpoints = app.MapGroup(OrdersRoute);

        ordersEndpoints.MapGet("/", async (ICrmModule crmModule) =>
        {
            var orders = await crmModule.ExecuteQuery(new GetOrdersQuery());

            return new GetOrdersResponse(orders
                .Select(o => o.Adapt<OrderDto>())
                .ToList());
        })
            .RequirePermission(OrdersPermissions.Read);

        ordersEndpoints.MapGet("/{orderId}", async (ICrmModule crmModule, Guid orderId) =>
        {
            var order = await crmModule.ExecuteQuery(new GetOrderQuery(orderId));

            return new GetOrderResponse(order?.Adapt<OrderDto>());
        })
            .RequirePermission(OrdersPermissions.Read);

        ordersEndpoints.MapGet("/details", async (ICrmModule crmModule, Guid[] orderId) =>
        {
            var orders = await crmModule.ExecuteQuery(new GetOrdersDetailsQuery(orderId));

            return new GetOrdersDetailsResponse(orders
                .Select(o => o.Adapt<OrderDetailsDto>())
                .ToList());
        })
            .RequirePermission(OrdersPermissions.Read);

        ordersEndpoints.MapGet("/detials/{orderId}", async (
            ICrmModule crmModule,
            Guid orderId) =>
        {
            var order = await crmModule.ExecuteQuery(new GetOrderDetailsQuery(orderId));

            return new GetOrderDetailsResponse(order?.Adapt<OrderDetailsDto>());
        })
            .RequirePermission(OrdersPermissions.Read);

        ordersEndpoints.MapPost("/", async (ICrmModule crmModule, CreateOrderRequest request) =>
        {
            var result = await crmModule.ExecuteCommand(
                request.Adapt<CreateOrderCommand>());

            return result.Match(
                orderId => Results.Ok(new CreateOrderResponse(orderId)),
                errors => errors.AsProblem());
        })
            .RequirePermission(OrdersPermissions.Write);

        ordersEndpoints.MapPut("/{orderId}", async (
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
        })
            .RequirePermission(OrdersPermissions.Write);

        ordersEndpoints.MapDelete("/{orderId}", async (ICrmModule crmModule, Guid orderId) =>
        {
            await crmModule.ExecuteCommand(new DeleteOrderCommand(orderId));
        })
            .RequirePermission(OrdersPermissions.Write);

        ordersEndpoints.MapPut("/{orderId}/cancel", async (ICrmModule crmModule, Guid orderId) =>
        {
            await crmModule.ExecuteCommand(new CancelOrderCommand(orderId));
        })
            .RequirePermission(OrdersPermissions.Write);

        ordersEndpoints.MapPut("/{orderId}/confirm", async (ICrmModule crmModule, Guid orderId) =>
        {
            await crmModule.ExecuteCommand(new ConfirmOrderCommand(orderId));
        })
            .RequirePermission(OrdersPermissions.Write);

        ordersEndpoints.MapPut("/{orderId}/complete", async (ICrmModule crmModule, Guid orderId) =>
        {
            await crmModule.ExecuteCommand(new CompleteOrderCommand(orderId));
        })
            .RequirePermission(OrdersPermissions.Write);
    }
}