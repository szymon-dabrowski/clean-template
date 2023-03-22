using Clean.Modules.Crm.Domain.Customers.Services;
using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Customers.Rules;
internal class CustomerTaxIdMustBeUniqueRule : IBusinessRule
{
    private readonly string taxId;
    private readonly ICustomerTaxIdUniquenessChecker customerTaxIdUniquenessChecker;

    public CustomerTaxIdMustBeUniqueRule(
        string taxId,
        ICustomerTaxIdUniquenessChecker customerTaxIdUniquenessChecker)
    {
        this.taxId = taxId;
        this.customerTaxIdUniquenessChecker = customerTaxIdUniquenessChecker;
    }

    public string Message => "Customer with given tax id already exists.";

    public async Task<bool> IsBroken() => !await customerTaxIdUniquenessChecker.IsUnique(taxId);
}