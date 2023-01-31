using Clean.Modules.Crm.Application.Orders.CreateOrder;
using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Crm.Application.Orders.UpdateOrder;
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

        config.NewConfig<OrderDto, Dto.Crm.Orders.Model.OrderDto>();

        config.NewConfig<OrderItemDto, Dto.Crm.Orders.Model.OrderItemDto>();

        config.NewConfig<OrderDetailsDto, Dto.Crm.Orders.Model.OrderDetailsDto>();

        config.NewConfig<OrderItemDetailsDto, Dto.Crm.Orders.Model.OrderItemDetailsDto>();

        config.NewConfig<OrderCustomerDetailsDto, Dto.Crm.Orders.Model.OrderCustomerDetailsDto>();
    }
}