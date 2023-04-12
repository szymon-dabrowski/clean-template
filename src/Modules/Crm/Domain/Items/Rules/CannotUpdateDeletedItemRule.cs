using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Items.Rules;
internal class CannotUpdateDeletedItemRule : IBusinessRule
{
    private readonly bool isDeleted;

    public CannotUpdateDeletedItemRule(bool isDeleted)
    {
        this.isDeleted = isDeleted;
    }

    public string Message => "Cannot update deleted item.";

    public Task<bool> IsBroken() => Task.FromResult(isDeleted);
}