using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Orders.GetOrdersDetails;
internal class GetOrdersDetailsQueryHandler : IQueryHandler<GetOrdersDetailsQuery, List<OrderDetailsDto>>
{
    private readonly DbContext dbContext;
    private readonly IMapper mapper;

    public GetOrdersDetailsQueryHandler(DbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<List<OrderDetailsDto>> Handle(
        GetOrdersDetailsQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Set<Order>()
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
            .Select(row => MapToDto(
                mapper,
                row.order,
                row.customer,
                row.items))
            .ToListAsync(cancellationToken);
    }

    private static OrderDetailsDto MapToDto(
        IMapper mapper,
        Order order,
        Customer customer,
        List<Item> items)
    {
        return mapper.Map<OrderDetailsDto>((
            order,
            customer,
            items));
    }
}