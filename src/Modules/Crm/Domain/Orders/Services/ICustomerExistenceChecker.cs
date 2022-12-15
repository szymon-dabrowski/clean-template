namespace Clean.Modules.Crm.Domain.Orders.Services;
public interface ICustomerExistenceChecker
{
    Task<bool> CustomerExists(Guid customerId);
}