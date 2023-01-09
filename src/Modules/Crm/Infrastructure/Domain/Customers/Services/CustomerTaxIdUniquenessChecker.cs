using Clean.Modules.Crm.Domain.Customers.Services;
using Clean.Modules.Crm.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Infrastructure.Domain.Customers.Services;
public class CustomerTaxIdUniquenessChecker : ICustomerTaxIdUniquenessChecker
{
    private readonly CrmContext crmContext;

    public CustomerTaxIdUniquenessChecker(CrmContext crmContext)
    {
        this.crmContext = crmContext;
    }

    public async Task<bool> IsUnique(string taxId)
    {
        return !await crmContext.Customers.AnyAsync(c => c.TaxId == taxId);
    }
}