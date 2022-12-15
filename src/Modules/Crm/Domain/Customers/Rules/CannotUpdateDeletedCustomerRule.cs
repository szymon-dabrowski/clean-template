using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Customers.Rules;
internal class CannotUpdateDeletedCustomerRule : IBussinesRule
{
    private readonly bool isDeleted;

    public CannotUpdateDeletedCustomerRule(bool isDeleted)
    {
        this.isDeleted = isDeleted;
    }

    public string Message => "Cannot update deleted customer.";

    public Task<bool> IsBroken() => Task.FromResult(isDeleted);
}