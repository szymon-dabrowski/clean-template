using Clean.Modules.Shared.Infrastructure.DependencyInjection;
using Clean.Modules.Shared.Infrastructure.DomainEventTypeMapping;
using Clean.Modules.Shared.Infrastructure.Idempotency;
using Clean.Modules.Shared.Persistence.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Crm.Infrastructure.Setup;
internal static class DependencyInjection
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMappings();

        services
            .RegisterCommandHandlersAsClosedTypes(typeof(Application.AssemblyMarker).Assembly)
            .DecorateCommandHandlersWithUnitOfWork();

        services.DecorateEventHandlersWithIdempotency();

        services.AddSingleton<IDomainEventTypeMapping>(_ =>
            new DomainEventTypeMapping(typeof(Domain.AssemblyMarker).Assembly));

        return services;
    }
}