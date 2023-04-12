using Clean.Modules.Crm.Domain.Customers;

namespace Clean.Modules.Crm.Domain.Orders.Services;
public interface ICustomerExistenceChecker
{
    Task<bool> CustomerExists(CustomerId customerId);
}