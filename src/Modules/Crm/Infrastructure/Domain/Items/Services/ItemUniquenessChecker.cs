using Clean.Modules.Crm.Domain.Items.Services;
using Clean.Modules.Crm.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Infrastructure.Domain.Items.Services;
internal class ItemUniquenessChecker : IItemUniquenessChecker
{
    private readonly CrmContext crmContext;

    public ItemUniquenessChecker(CrmContext crmContext)
    {
        this.crmContext = crmContext;
    }

    public async Task<bool> IsUnique(string name)
    {
        return !await crmContext.Items.AnyAsync(i => i.Name == name);
    }
}