using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Crm.Domain.Customers;
using Mapster;

namespace Clean.Modules.Crm.Application.Orders.Mappings;
internal class OrderCustomerDetialsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Customer, OrderCustomerDetailsDto>()
            .Map(d => d.CustomerId, c => c.Id.Value);
    }
}