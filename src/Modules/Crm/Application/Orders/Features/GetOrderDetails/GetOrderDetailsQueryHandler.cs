using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Crm.Dto.Queries.Orders;
using Clean.Modules.Crm.Dto.Queries.Orders.Model;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Orders.Features.GetOrderDetails;
internal class GetOrderDetailsQueryHandler : IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto?>
{
    private readonly DbContext dbContext;
    private readonly IMapper mapper;

    public GetOrderDetailsQueryHandler(DbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
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
            .FirstOrDefaultAsync(cancellationToken);

        return orderDetials == null
            ? null
            : mapper.Map<OrderDetailsDto>((
                orderDetials.order,
                orderDetials.customer,
                orderDetials.items));
    }
}