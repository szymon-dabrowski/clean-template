using Clean.Modules.Crm.Domain.Customers.Services;
using Clean.Modules.Crm.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Infrastructure.Domain.Customers.Services;
internal class CustomerNameUniquenessChecker : ICustomerNameUniquenessChecker
{
    private readonly CrmContext crmContext;

    public CustomerNameUniquenessChecker(CrmContext crmContext)
    {
        this.crmContext = crmContext;
    }

    public async Task<bool> IsUnique(string name)
    {
        return !await crmContext.Customers.AnyAsync(c => c.Name == name);
    }
}