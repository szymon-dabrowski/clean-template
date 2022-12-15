using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders.Rules;
internal class OrderMustContainUniqueOrderItemsRule : IBussinesRule
{
    private readonly IEnumerable<OrderItem> orderItems;

    public OrderMustContainUniqueOrderItemsRule(IEnumerable<OrderItem> orderItems)
    {
        this.orderItems = orderItems;
    }

    public string Message => "Order cannot contain duplicated items.";

    public Task<bool> IsBroken() =>
        Task.FromResult(orderItems
            .GroupBy(i => new { i.ItemId, i.PricePerUnit })
            .Any(grp => grp.Count() > 1));
}