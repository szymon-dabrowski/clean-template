using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders.Rules;
internal class CannotUpdateDeletedOrderRule : IBusinessRule
{
    private readonly bool isDeleted;

    public CannotUpdateDeletedOrderRule(bool isDeleted)
    {
        this.isDeleted = isDeleted;
    }

    public string Message => "Cannot update deleted order.";

    public Task<bool> IsBroken() => Task.FromResult(isDeleted);
}