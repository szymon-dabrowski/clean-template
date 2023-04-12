using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Orders;
using Mapster;

namespace Clean.Modules.Crm.Application.Orders.Mappings;
internal class OrderItemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderItemDto, OrderItem>()
            .MapWith(oi => OrderItem.Create(
                new ItemId(oi.ItemId),
                oi.Quantity,
                oi.PricePerUnit));

        config.NewConfig<OrderItem, OrderItemDto>()
            .Map(d => d.ItemId, s => s.ItemId.Value);
    }
}