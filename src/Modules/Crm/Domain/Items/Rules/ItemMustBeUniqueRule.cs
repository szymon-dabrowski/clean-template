using Clean.Modules.Crm.Domain.Items.Services;
using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Items.Rules;
internal class ItemMustBeUniqueRule : IBussinesRule
{
    private readonly string name;
    private readonly IItemUniquenessChecker itemUniquenessChecker;

    public ItemMustBeUniqueRule(
        string name,
        IItemUniquenessChecker itemUniquenessChecker)
    {
        this.name = name;
        this.itemUniquenessChecker = itemUniquenessChecker;
    }

    public string Message => "Item with given name already exists.";

    public async Task<bool> IsBroken() => !await itemUniquenessChecker.IsUnique(name);
}