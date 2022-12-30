using Clean.Modules.Crm.Domain.Items;

namespace Clean.Modules.Crm.Domain.Customers;
public interface ICustomerRepository
{
    Task Add(Customer customer);

    Task<Customer?> GetById(Guid id);
}