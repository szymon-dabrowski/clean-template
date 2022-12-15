using Clean.Modules.Crm.Domain.Money;
using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders;
public class OrderItem : Entity<int>
{
    private OrderItem(
        Guid itemId,
        decimal quantity,
        PriceValueObject pricePerUnit)
        : base(default)
    {
        ItemId = itemId;
        Quantity = quantity;
        PricePerUnit = pricePerUnit;
    }

    public Guid ItemId { get; private set; }

    public decimal Quantity { get; private set; }

    public PriceValueObject PricePerUnit { get; private set; }

    public static OrderItem Create(
        Guid itemId,
        decimal quanity,
        PriceValueObject pricePerUnit)
    {
        return new OrderItem(
            itemId,
            quanity,
            pricePerUnit);
    }
}