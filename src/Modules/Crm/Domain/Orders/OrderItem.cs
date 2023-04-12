using Clean.Modules.Crm.Domain.Items;

namespace Clean.Modules.Crm.Domain.Orders;
public record OrderItem
{
    private OrderItem(
        ItemId itemId,
        decimal quantity,
        decimal pricePerUnit)
    {
        ItemId = itemId;
        Quantity = quantity;
        PricePerUnit = pricePerUnit;
    }

    public ItemId ItemId { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal PricePerUnit { get; private set; }

    public static OrderItem Create(
        ItemId itemId,
        decimal quanity,
        decimal pricePerUnit)
    {
        return new OrderItem(
            itemId,
            quanity,
            pricePerUnit);
    }
}