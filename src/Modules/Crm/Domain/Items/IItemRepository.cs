namespace Clean.Modules.Crm.Domain.Items;
public interface IItemRepository
{
    Task Add(Item item);

    Task<Item?> GetById(Guid id);
}