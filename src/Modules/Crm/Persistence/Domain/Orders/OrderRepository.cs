using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Crm.Persistence.Database;

namespace Clean.Modules.Crm.Persistence.Domain.Orders;
internal class OrderRepository : IOrderRepository
{
    private readonly CrmContext crmContext;

    public OrderRepository(CrmContext crmContext)
    {
        this.crmContext = crmContext;
    }

    public async Task Add(Order order)
    {
        await crmContext.Orders.AddAsync(order);
    }

    public async Task<Order?> GetById(Guid id)
    {
        return await crmContext.Orders.FindAsync(id);
    }
}