using Clean.Modules.Crm.Application.Customers.Dto;
using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Customers.GetCustomers;
internal class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, List<CustomerDto>>
{
    private readonly DbContext dbContext;

    public GetCustomersQueryHandler(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<CustomerDto>> Handle(
        GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Set<Customer>()
            .Select(c => c.Adapt<CustomerDto>())
            .ToListAsync(cancellationToken);
    }
}