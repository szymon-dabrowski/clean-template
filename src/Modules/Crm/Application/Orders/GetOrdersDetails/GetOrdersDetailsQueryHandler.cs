﻿using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Orders.GetOrdersDetails;
internal class GetOrdersDetailsQueryHandler : IQueryHandler<GetOrdersDetailsQuery, List<OrderDetailsDto>>
{
    private readonly DbContext dbContext;

    public GetOrdersDetailsQueryHandler(DbContext dbContext)
    {
        this.dbContext = dbContext;
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
                        .IgnoreQueryFilters()
                        .Where(i => o.OrderItems
                            .Select(oi => oi.ItemId)
                            .Contains(i.Id))
                            .ToList(),
                })
            .Where(row => !request.OrderIds.Any() ||
                request.OrderIds
                    .Select(id => new OrderId(id))
                    .Contains(row.order.Id))
            .Select(row => MapToDto(row.order, row.customer, row.items))
            .ToListAsync(cancellationToken);
    }

    private static OrderDetailsDto MapToDto(Order order, Customer customer, List<Item> items)
        => (order, customer, items).Adapt<OrderDetailsDto>();
}