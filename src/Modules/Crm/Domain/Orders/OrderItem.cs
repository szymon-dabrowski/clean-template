using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders;
public class OrderItem : ValueObject
{
    private OrderItem(
        Guid itemId,
        decimal quantity,
        decimal pricePerUnit)
    {
        ItemId = itemId;
        Quantity = quantity;
        PricePerUnit = pricePerUnit;
    }

    public Guid ItemId { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal PricePerUnit { get; private set; }

    public static OrderItem Create(
        Guid itemId,
        decimal quanity,
        decimal pricePerUnit)
    {
        return new OrderItem(
            itemId,
            quanity,
            pricePerUnit);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ItemId;
        yield return Quantity;
        yield return PricePerUnit;
    }
}