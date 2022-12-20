namespace Clean.Modules.Crm.Domain.Items;
public interface IItemRepository
{
    Task Add(Item order);

    Task<Item> GetById(Guid id);
}