using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Dto.Queries.Customers.Model;
using Mapster;

namespace Clean.Modules.Crm.Application.Customers.Mappings;
internal class CustomerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Customer, CustomerDto>();
    }
}