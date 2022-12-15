using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders.Rules;
internal class ItemsMustExistRule : IBussinesRule
{
    private readonly IEnumerable<Guid> itemIds;
    private readonly IItemExistenceChecker itemsExistenceChecker;

    public ItemsMustExistRule(
        IEnumerable<Guid> itemIds,
        IItemExistenceChecker itemsExistenceChecker)
    {
        this.itemIds = itemIds;
        this.itemsExistenceChecker = itemsExistenceChecker;
    }

    public string Message => "Cannot create order with non existing item.";

    public async Task<bool> IsBroken() => !await itemsExistenceChecker.ItemsExists(itemIds);
}