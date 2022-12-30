using Clean.Modules.Crm.Domain.Orders;
using Mapster;

namespace Clean.Modules.Crm.Infrastructure.Mappings.Orders;
internal class OrderItemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Dto.Commands.Orders.Model.OrderItem, OrderItem>()
            .MapWith(oi => OrderItem.Create(
                oi.ItemId,
                oi.Quantity,
                oi.PricePerUnit));
    }
}