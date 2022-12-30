using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Crm.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Infrastructure.Domain.Orders.Services;
internal class CustomerExistenceChecker : ICustomerExistenceChecker
{
    private readonly CrmContext crmContext;

    public CustomerExistenceChecker(CrmContext crmContext)
    {
        this.crmContext = crmContext;
    }

    public async Task<bool> CustomerExists(Guid customerId)
    {
        return await crmContext.Customers.AnyAsync(c => c.Id == customerId);
    }
}