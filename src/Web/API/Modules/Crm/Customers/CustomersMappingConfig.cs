using Clean.Modules.Crm.Application.Customers.CreateCustomer;
using Clean.Modules.Crm.Application.Customers.Dto;
using Clean.Modules.Crm.Application.Customers.UpdateCustomer;
using Clean.Web.Dto.Crm.Customers.Requests;
using Mapster;

namespace Clean.Web.Api.Modules.Crm.Customers;

public class CustomersMappingConfig : IRegister
{
    public const string CustomerIdParameter = "CustomerId";

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCustomerRequest, CreateCustomerCommand>();

        config.NewConfig<UpdateCustomerRequest, UpdateCustomerCommand>()
            .Map(
                dest => dest.CustomerId,
                _ => (Guid)MapContext.Current!.Parameters[CustomerIdParameter]);

        config.NewConfig<CustomerDto, Dto.Crm.Customers.Model.CustomerDto>();
    }
}