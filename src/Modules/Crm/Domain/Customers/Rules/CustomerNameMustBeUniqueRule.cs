using Clean.Modules.Crm.Domain.Customers.Services;
using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Customers.Rules;
internal class CustomerNameMustBeUniqueRule : IBusinessRule
{
    private readonly string name;
    private readonly ICustomerNameUniquenessChecker customerUniquenessChecker;

    public CustomerNameMustBeUniqueRule(
        string name,
        ICustomerNameUniquenessChecker customerUniquenessChecker)
    {
        this.name = name;
        this.customerUniquenessChecker = customerUniquenessChecker;
    }

    public string Message => "Customer with given name already exists.";

    public async Task<bool> IsBroken() => !await customerUniquenessChecker.IsUnique(name);
}