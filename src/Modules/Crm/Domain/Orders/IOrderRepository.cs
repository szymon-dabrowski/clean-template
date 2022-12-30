namespace Clean.Modules.Crm.Domain.Orders;
public interface IOrderRepository
{
    Task Add(Order order);

    Task<Order?> GetById(Guid id);
}