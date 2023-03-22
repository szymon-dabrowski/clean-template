using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Orders.GetOrder;
internal class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderDto?>
{
    private readonly DbContext dbContext;

    public GetOrderQueryHandler(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<OrderDto?> Handle(
        GetOrderQuery request,
        CancellationToken cancellationToken)
    {
        var order = await dbContext.Set<Order>()
            .FirstOrDefaultAsync(o => o.Id == new OrderId(request.OrderId), cancellationToken);

        return order == null
            ? null
            : order.Adapt<OrderDto>();
    }
}