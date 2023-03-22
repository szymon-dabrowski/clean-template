using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Crm.Domain.Orders;
using Mapster;

namespace Clean.Modules.Crm.Application.Orders.Mappings;
internal class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderDto>()
            .Map(d => d.Id, s => s.Id.Value);
    }
}