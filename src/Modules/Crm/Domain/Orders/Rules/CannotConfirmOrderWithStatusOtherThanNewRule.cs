using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders.Rules;
internal class CannotConfirmOrderWithStatusOtherThanNewRule : IBussinesRule
{
    private readonly OrderStatus orderStatus;

    public CannotConfirmOrderWithStatusOtherThanNewRule(OrderStatus orderStatus)
    {
        this.orderStatus = orderStatus;
    }

    public string Message => $"Cannot confirm order with {orderStatus} status, " +
        $"{OrderStatus.New} expected.";

    public Task<bool> IsBroken()
        => Task.FromResult(orderStatus != OrderStatus.New);
}