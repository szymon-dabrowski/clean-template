using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Orders.GetOrderDetails;
internal class GetOrderDetailsQueryHandler : IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto?>
{
    private readonly DbContext dbContext;

    public GetOrderDetailsQueryHandler(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<OrderDetailsDto?> Handle(
        GetOrderDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var orderDetials = await dbContext.Set<Order>()
            .Join(
                dbContext.Set<Customer>(),
                o => o.CustomerId,
                c => c.Id,
                (o, c) => new
                {
                    order = o,
                    customer = c,
                    items = dbContext.Set<Item>()
                        .Where(i => o.OrderItems
                            .Select(oi => oi.ItemId)
                            .Contains(i.Id))
                            .ToList(),
                })
            .FirstOrDefaultAsync(row => row.order.Id == request.OrderId, cancellationToken);

        return orderDetials == null
            ? null
            : (orderDetials.order, orderDetials.customer, orderDetials.items)
                .Adapt<OrderDetailsDto>();
    }
}