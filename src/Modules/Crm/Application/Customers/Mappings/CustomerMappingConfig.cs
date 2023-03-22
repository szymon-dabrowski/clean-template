using Clean.Modules.Crm.Application.Customers.Dto;
using Clean.Modules.Crm.Domain.Customers;
using Mapster;

namespace Clean.Modules.Crm.Application.Customers.Mappings;
internal class CustomerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Customer, CustomerDto>()
            .Map(d => d.Id, s => s.Id.Value);
    }
}