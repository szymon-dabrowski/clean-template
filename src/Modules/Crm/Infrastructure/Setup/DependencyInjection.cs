using Clean.Modules.Crm.Domain.Customers.Services;
using Clean.Modules.Crm.Domain.Items.Services;
using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Crm.Infrastructure.Domain.Customers.Services;
using Clean.Modules.Crm.Infrastructure.Domain.Items.Services;
using Clean.Modules.Crm.Infrastructure.Domain.Orders.Services;
using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Infrastructure.DomainEventTypeMapping;
using Clean.Modules.Shared.Infrastructure.Idempotency;
using Clean.Modules.Shared.Infrastructure.Services;
using Clean.Modules.Shared.Persistence.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Crm.Infrastructure.Setup;
internal static class DependencyInjection
{
    internal static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IExecutionContextAccessor executionContextAccessor)
    {
        services.DecorateCommandHandlersWithUnitOfWork();

        services.DecorateEventHandlersWithIdempotency();

        services.AddSingleton<IDomainEventTypeMapping>(_ =>
            new DomainEventTypeMapping(typeof(Crm.Domain.AssemblyMarker).Assembly));

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddSingleton(executionContextAccessor);

        // TODO - add some scanning for domain services
        services.AddScoped<ICustomerNameUniquenessChecker, CustomerNameUniquenessChecker>();
        services.AddScoped<ICustomerTaxIdUniquenessChecker, CustomerTaxIdUniquenessChecker>();

        services.AddScoped<IItemUniquenessChecker, ItemUniquenessChecker>();

        services.AddScoped<ICustomerExistenceChecker, CustomerExistenceChecker>();
        services.AddScoped<IItemExistenceChecker, ItemExistenceChecker>();
        services.AddScoped<IOrderNumberGenerator, OrderNumberGenerator>();
        services.AddScoped<IOrderNumberUniquenessChecker, OrderNumberUniquenessChecker>();

        return services;
    }
}