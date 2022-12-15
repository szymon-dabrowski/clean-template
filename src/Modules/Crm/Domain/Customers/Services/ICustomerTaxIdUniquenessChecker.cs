namespace Clean.Modules.Crm.Domain.Customers.Services;
public interface ICustomerTaxIdUniquenessChecker
{
    Task<bool> IsUnique(string taxId);
}