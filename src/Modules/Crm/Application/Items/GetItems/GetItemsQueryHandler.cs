using Clean.Modules.Crm.Application.Items.Dto;
using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Items.GetItems;
internal class GetItemsQueryHandler : IQueryHandler<GetItemsQuery, List<ItemDto>>
{
    private readonly DbContext dbContext;

    public GetItemsQueryHandler(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<ItemDto>> Handle(
        GetItemsQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Set<Item>()
            .Select(i => i.Adapt<ItemDto>())
            .ToListAsync(cancellationToken);
    }
}