using Clean.Modules.Crm.Dto.Commands.Customers;
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
    }
}