using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders.Rules;
internal class CannotCompleteOrderWithStatusOtherThanPendingPaymentRule : IBusinessRule
{
    private readonly OrderStatus orderStatus;

    public CannotCompleteOrderWithStatusOtherThanPendingPaymentRule(OrderStatus orderStatus)
    {
        this.orderStatus = orderStatus;
    }

    public string Message => $"Cannot complete order with {orderStatus} status, " +
        $"{OrderStatus.PendingPayment} expected.";

    public Task<bool> IsBroken()
        => Task.FromResult(orderStatus != OrderStatus.PendingPayment);
}