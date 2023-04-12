using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders.Rules;
internal class ItemsMustExistRule : IBusinessRule
{
    private readonly IEnumerable<ItemId> itemIds;
    private readonly IItemExistenceChecker itemsExistenceChecker;

    public ItemsMustExistRule(
        IEnumerable<ItemId> itemIds,
        IItemExistenceChecker itemsExistenceChecker)
    {
        this.itemIds = itemIds;
        this.itemsExistenceChecker = itemsExistenceChecker;
    }

    public string Message => "Cannot create order with non existing item.";

    public async Task<bool> IsBroken() => !await itemsExistenceChecker.ItemsExists(itemIds);
}