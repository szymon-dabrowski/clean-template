using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Persistence.Database;

namespace Clean.Modules.Crm.Persistence.Domain.Items;
internal class ItemRepository : IItemRepository
{
    private readonly CrmContext crmContext;

    public ItemRepository(CrmContext crmContext)
    {
        this.crmContext = crmContext;
    }

    public async Task Add(Item item)
    {
        await crmContext.Items.AddAsync(item);
    }

    public async Task<Item?> GetById(Guid id)
    {
        return await crmContext.Items.FindAsync(id);
    }
}