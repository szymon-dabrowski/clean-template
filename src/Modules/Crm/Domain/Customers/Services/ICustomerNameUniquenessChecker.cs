namespace Clean.Modules.Crm.Domain.Customers.Services;
public interface ICustomerNameUniquenessChecker
{
    Task<bool> IsUnique(string name);
}