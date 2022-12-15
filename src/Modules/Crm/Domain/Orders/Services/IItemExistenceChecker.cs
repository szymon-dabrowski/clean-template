namespace Clean.Modules.Crm.Domain.Orders.Services;
public interface IItemExistenceChecker
{
    Task<bool> ItemsExists(IEnumerable<Guid> itemIds);
}