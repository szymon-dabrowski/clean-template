using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Orders;
using Mapster;

namespace Clean.Modules.Crm.Application.Orders.Mappings;
internal class OrderDetialsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Order Order, Customer Customer, List<Item> Items), OrderDetailsDto>()
            .Map(d => d.OrderId, s => s.Order.Id.Value)
            .Map(d => d.Currency, s => s.Order.Currency)
            .Map(d => d.OrderNumber, s => s.Order.OrderNumber)
            .Map(d => d.OrderDate, s => s.Order.OrderDate)
            .Map(d => d.Customer, s => s.Customer)
            .Map(d => d.OrderItems, s => s.Order.OrderItems
                .Select(oi => ToTuple(oi, s.Items)));
    }

    private static (OrderItem OrderItem, Item Item) ToTuple(OrderItem orderItem, List<Item> items)
    {
        return (orderItem, items.First(i => i.Id == orderItem.ItemId));
    }
}