using Clean.Modules.Crm.Application.Orders.CreateOrder;
using Clean.Modules.Crm.Application.Orders.DeleteOrder;
using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Crm.Application.Orders.GetOrder;
using Clean.Modules.Crm.Application.Orders.GetOrderDetails;
using Clean.Modules.Crm.Application.Orders.GetOrders;
using Clean.Modules.Crm.Application.Orders.GetOrdersDetails;
using Clean.Modules.Crm.Application.Orders.UpdateOrder;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Web.Api.Common.Controllers;
using Clean.Web.Dto.Crm.Orders.Requests;
using Clean.Web.Dto.Crm.Orders.Responses;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.Api.Modules.Crm.Orders;

[Route("[controller]")]
public class OrdersController : ApiController
{
    private readonly ICrmModule crmModule;

    public OrdersController(ICrmModule crmModule)
    {
        this.crmModule = crmModule;
    }

    [HttpGet]
    public async Task<List<OrderDto>> GetOrders()
        => await crmModule.ExecuteQuery(new GetOrdersQuery());

    [HttpGet("{orderId}")]
    public async Task<OrderDto?> GetOrder(Guid orderId)
        => await crmModule.ExecuteQuery(new GetOrderQuery(orderId));

    [HttpGet("details")]
    public async Task<List<OrderDetailsDto>> GetOrdersDetails([FromQuery] List<Guid> orderIds)
        => await crmModule.ExecuteQuery(new GetOrdersDetailsQuery(orderIds));

    [HttpGet("{orderId}/details")]
    public async Task<OrderDetailsDto?> GetOrderDetails(Guid orderId)
        => await crmModule.ExecuteQuery(new GetOrderDetailsQuery(orderId));

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        var result = await crmModule.ExecuteCommand(
            request.Adapt<CreateOrderCommand>());

        return result.Match(
            orderId => Ok(new CreateOrderResponse(orderId)),
            errors => Problem(errors));
    }

    [HttpPut("orderId")]
    public async Task<IActionResult> UpdateOrder(Guid orderId, UpdateOrderRequest request)
    {
        var result = await crmModule.ExecuteCommand(
            request.BuildAdapter()
                .AddParameters(OrdersMappingConfig.OrderIdParam, orderId)
                .AdaptToType<UpdateOrderCommand>());

        return result.Match(
            _ => Ok(),
            errors => Problem(errors));
    }

    [HttpDelete("{orderId}")]
    public async Task DeleteOrder(Guid orderId)
        => await crmModule.ExecuteCommand(new DeleteOrderCommand(orderId));
}