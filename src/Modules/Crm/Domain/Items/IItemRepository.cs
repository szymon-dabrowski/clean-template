namespace Clean.Modules.Crm.Domain.Items;
public interface IItemRepository
{
    Task Add(Item item);

    Task<Item?> GetById(ItemId id);
}