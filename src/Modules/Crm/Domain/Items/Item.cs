using Clean.Modules.Crm.Domain.Items.Rules;
using Clean.Modules.Crm.Domain.Items.Services;
using Clean.Modules.Crm.Domain.Money;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Items;
public class Item : AggregateRoot<Guid>
{
    private Item(
        Guid id,
        string name,
        string description,
        PriceValueObject basePrice)
        : base(id)
    {
        Name = name;
        Description = description;
        BasePrice = basePrice;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public PriceValueObject BasePrice { get; private set; }

    public bool IsDeleted { get; private set; }

    internal static async Task<ErrorOr<Item>> Create(
        string name,
        string description,
        PriceValueObject basePrice,
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
            basePrice);
    }

    internal async Task<ErrorOr<Item>> Update(
        string name,
        string description,
        PriceValueObject basePrice,
        IItemUniquenessChecker itemUniquenessChecker)
    {
        var result = await Check(
            new CannotUpdateDeletedItemRule(IsDeleted),
            new ItemMustBeUniqueRule(name, itemUniquenessChecker));

        if (result.IsError)
        {
            return Error.From(result);
        }

        Name = name;
        Description = description;
        BasePrice = basePrice;

        return this;
    }

    internal void Delete()
    {
        IsDeleted = true;
    }
}