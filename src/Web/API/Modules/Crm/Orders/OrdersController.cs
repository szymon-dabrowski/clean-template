using Clean.Modules.Crm.Dto.Commands.Orders;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Web.Api.Common.Controllers;
using Clean.Web.Dto.Crm.Orders.Requests;
using Clean.Web.Dto.Crm.Orders.Responses;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.Api.Modules.Crm.Orders;

[Route("[controller]")]
public class OrdersController : ApiController
{
    private readonly ICrmModule crmModule;
    private readonly IMapper mapper;

    public OrdersController(ICrmModule crmModule, IMapper mapper)
    {
        this.crmModule = crmModule;
        this.mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        var result = await crmModule.ExecuteCommand(
            mapper.Map<CreateOrderCommand>(request));

        return result.Match(
            orderId => Ok(new CreateOrderResponse(orderId)),
            errors => Problem(errors));
    }

    [HttpPut("orderId")]
    public async Task<IActionResult> UpdateOrder(Guid orderId, UpdateOrderRequest request)
    {
        var result = await crmModule.ExecuteCommand(
            mapper.From(request)
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