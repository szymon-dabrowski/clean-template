using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Persistence.Database;

namespace Clean.Modules.Crm.Persistence.Domain.Customers;
internal class CustomerRepository : ICustomerRepository
{
    private readonly CrmContext crmContext;

    public CustomerRepository(CrmContext crmContext)
    {
        this.crmContext = crmContext;
    }

    public async Task Add(Customer customer)
    {
        await crmContext.AddAsync(customer);
    }

    public async Task<Customer?> GetById(CustomerId id)
    {
        return await crmContext.Customers.FindAsync(id);
    }
}