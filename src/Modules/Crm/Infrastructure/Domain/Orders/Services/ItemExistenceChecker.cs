using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Crm.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Infrastructure.Domain.Orders.Services;
internal class ItemExistenceChecker : IItemExistenceChecker
{
    private readonly CrmContext crmContext;

    public ItemExistenceChecker(CrmContext crmContext)
    {
        this.crmContext = crmContext;
    }

    public async Task<bool> ItemsExists(IEnumerable<Guid> itemIds)
    {
        var uniqueItemsIds = itemIds.Distinct();

        var existingItemsCount = await crmContext.Items
            .Where(i => uniqueItemsIds.Contains(i.Id))
            .CountAsync();

        return uniqueItemsIds.Count() == existingItemsCount;
    }
}