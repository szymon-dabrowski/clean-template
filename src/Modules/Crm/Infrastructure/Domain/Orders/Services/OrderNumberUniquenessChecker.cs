using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Crm.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Infrastructure.Domain.Orders.Services;
internal class OrderNumberUniquenessChecker : IOrderNumberUniquenessChecker
{
    private readonly CrmContext crmContext;

    public OrderNumberUniquenessChecker(CrmContext crmContext)
    {
        this.crmContext = crmContext;
    }

    public async Task<bool> IsUnique(string orderNumber)
    {
        return !await crmContext.Orders.AnyAsync(o => o.OrderNumber == orderNumber);
    }
}