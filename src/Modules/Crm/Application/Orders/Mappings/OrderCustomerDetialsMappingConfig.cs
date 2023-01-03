using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Dto.Queries.Orders.Model;
using Mapster;

namespace Clean.Modules.Crm.Application.Orders.Mappings;
internal class OrderCustomerDetialsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Customer, OrderCustomerDetailsDto>()
            .Map(d => d.CustomerId, c => c.Id);
    }
}