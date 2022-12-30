using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Crm.Persistence.Database;
using Clean.Modules.Shared.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Infrastructure.Domain.Orders.Services;
internal class OrderNumberGenerator : IOrderNumberGenerator
{
    private readonly CrmContext crmContext;
    private readonly IDateTimeProvider dateTimeProvider;

    public OrderNumberGenerator(CrmContext crmContext, IDateTimeProvider dateTimeProvider)
    {
        this.crmContext = crmContext;
        this.dateTimeProvider = dateTimeProvider;
    }

    public async Task<string> GenerateOrderNumber()
    {
        var currentMonth = dateTimeProvider.UtcNow.Month;

        var currentMonthOrdersCount = await crmContext.Orders
            .Where(o => o.OrderDate.Month == currentMonth)
            .CountAsync();

        return FormatOrderNumber(currentMonthOrdersCount);
    }

    private string FormatOrderNumber(int count)
    {
        return $"{count}/{dateTimeProvider.UtcNow.Month}/{dateTimeProvider.UtcNow.Year}";
    }
}