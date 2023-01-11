using Clean.Modules.Crm.Application.Customers.Dto;
using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Customers.GetCustomer;
internal class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, CustomerDto?>
{
    private readonly DbContext dbContext;

    public GetCustomerQueryHandler(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<CustomerDto?> Handle(
        GetCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await dbContext.Set<Customer>()
            .FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken);

        return customer == null
            ? null
            : customer.Adapt<CustomerDto>();
    }
}