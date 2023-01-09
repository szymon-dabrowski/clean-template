using Clean.Modules.Crm.Application.Customers.Dto;
using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Customers.GetCustomers;
internal class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, List<CustomerDto>>
{
    private readonly DbContext dbContext;
    private readonly IMapper mapper;

    public GetCustomersQueryHandler(DbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<List<CustomerDto>> Handle(
        GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Set<Customer>()
            .Select(c => mapper.Map<CustomerDto>(c))
            .ToListAsync(cancellationToken);
    }
}