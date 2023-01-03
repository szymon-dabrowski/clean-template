using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Dto.Queries.Customers;
using Clean.Modules.Crm.Dto.Queries.Customers.Model;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Customers.Features.GetCustomer;
internal class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, CustomerDto?>
{
    private readonly DbContext dbContext;
    private readonly IMapper mapper;

    public GetCustomerQueryHandler(DbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<CustomerDto?> Handle(
        GetCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await dbContext.Set<Customer>()
            .FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken);

        return customer == null
            ? null
            : mapper.Map<CustomerDto>(customer);
    }
}