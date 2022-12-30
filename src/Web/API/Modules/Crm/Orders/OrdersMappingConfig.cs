using Clean.Modules.Crm.Dto.Commands.Orders;
using Clean.Web.Dto.Crm.Orders.Requests;
using Mapster;

namespace Clean.Web.Api.Modules.Crm.Orders;

public class OrdersMappingConfig : IRegister
{
    public const string OrderIdParam = "OrderId";

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateOrderRequest, CreateOrderCommand>();

        config.NewConfig<UpdateOrderRequest, UpdateOrderCommand>()
            .Map(
                dest => dest.OrderId,
                _ => (Guid)MapContext.Current!.Parameters[OrderIdParam]);
    }
}