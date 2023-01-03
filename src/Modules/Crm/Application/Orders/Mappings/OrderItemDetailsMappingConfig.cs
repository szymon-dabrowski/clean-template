using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Crm.Dto.Queries.Orders.Model;
using Mapster;

namespace Clean.Modules.Crm.Application.Orders.Mappings;
internal class OrderItemDetailsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(OrderItem OrderItem, Item Item), OrderItemDetailsDto>()
            .Map(d => d.ItemId, i => i.OrderItem.ItemId)
            .Map(d => d.Name, i => i.Item.Name)
            .Map(d => d.Description, i => i.Item.Description)
            .Map(d => d.BaseCurrency, i => i.Item.BaseCurrency)
            .Map(d => d.BasePrice, i => i.Item.BasePrice)
            .Map(d => d.Quantity, i => i.OrderItem.Quantity)
            .Map(d => d.PricePerUnit, i => i.OrderItem.PricePerUnit);
    }
}