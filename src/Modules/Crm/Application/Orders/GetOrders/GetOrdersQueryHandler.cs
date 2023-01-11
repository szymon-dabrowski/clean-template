using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Orders.GetOrders;
internal class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, List<OrderDto>>
{
    private readonly DbContext dbContext;

    public GetOrdersQueryHandler(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<OrderDto>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Set<Order>()
            .Select(o => o.Adapt<OrderDto>())
            .ToListAsync(cancellationToken);
    }
}