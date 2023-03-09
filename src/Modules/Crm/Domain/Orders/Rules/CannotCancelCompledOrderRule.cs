using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders.Rules;
internal class CannotCancelCompledOrderRule : IBussinesRule
{
    private readonly OrderStatus orderStatus;

    public CannotCancelCompledOrderRule(OrderStatus orderStatus)
    {
        this.orderStatus = orderStatus;
    }

    public string Message => $"Cannot cancel order with {orderStatus} status, " +
        $"{OrderStatus.New} or {OrderStatus.PendingPayment} expected.";

    public Task<bool> IsBroken()
        => Task.FromResult(orderStatus != OrderStatus.New);
}