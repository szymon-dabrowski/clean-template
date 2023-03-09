using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders.Rules;
internal class CannotUpdateOrderWithStatusOtherThanNewRule : IBussinesRule
{
    private readonly OrderStatus orderStatus;

    public CannotUpdateOrderWithStatusOtherThanNewRule(OrderStatus orderStatus)
    {
        this.orderStatus = orderStatus;
    }

    public string Message => $"Cannot update order in {orderStatus} status, " +
        $"{OrderStatus.New} expected.";

    public Task<bool> IsBroken()
    {
        return Task.FromResult(orderStatus != OrderStatus.New);
    }
}