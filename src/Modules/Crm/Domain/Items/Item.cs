using Clean.Modules.Crm.Domain.Items.Rules;
using Clean.Modules.Crm.Domain.Items.Services;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Items;
public class Item : AuditableAggregateRoot<Guid>
{
    private Item(
        Guid id,
        string name,
        string description,
        decimal basePrice,
        string baseCurrency)
        : base(id)
    {
        Name = name;
        Description = description;
        BasePrice = basePrice;
        BaseCurrency = baseCurrency;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public decimal BasePrice { get; private set; }

    public string BaseCurrency { get; set; }

    public bool IsDeleted { get; private set; }

    public static async Task<ErrorOr<Item>> Create(
        string name,
        string description,
        decimal basePrice,
        string baseCurrency,
        IItemUniquenessChecker itemUniquenessChecker)
    {
        var result = await Check(new ItemMustBeUniqueRule(name, itemUniquenessChecker));

        if (result.IsError)
        {
            return Error.From(result);
        }

        return new Item(
            Guid.NewGuid(),
            name,
            description,
            basePrice,
            baseCurrency);
    }

    public async Task<ErrorOr<Item>> Update(
        string name,
        string description,
        decimal basePrice,
        string baseCurrency,
        IItemUniquenessChecker itemUniquenessChecker)
    {
        var rulesToCheck = new List<IBussinesRule>()
        {
            new CannotUpdateDeletedItemRule(IsDeleted),
        };

        if (Name != name)
        {
            rulesToCheck.Add(new ItemMustBeUniqueRule(name, itemUniquenessChecker));
        }

        var result = await Check(rulesToCheck.ToArray());

        if (result.IsError)
        {
            return Error.From(result);
        }

        Name = name;
        Description = description;
        BasePrice = basePrice;
        BaseCurrency = baseCurrency;

        return this;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}