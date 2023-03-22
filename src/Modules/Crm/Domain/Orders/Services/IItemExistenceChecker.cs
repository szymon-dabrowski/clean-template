using Clean.Modules.Crm.Domain.Items;

namespace Clean.Modules.Crm.Domain.Orders.Services;
public interface IItemExistenceChecker
{
    Task<bool> ItemsExists(IEnumerable<ItemId> itemIds);
}