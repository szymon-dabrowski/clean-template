using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Domain;
using Clean.Modules.Shared.Infrastructure;
using Clean.Modules.Shared.Infrastructure.Idempotency;
using Clean.Modules.Shared.IntegrationTests.SeedWork;
using Clean.Modules.Shared.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Shared.IntegrationTests.Idempotency;
internal static class DependencyInjection
{
    public static IServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();

        var assembly = typeof(DependencyInjection).Assembly;

        services.AddSingleton<IDateTimeProvider, TestDateTimeProvider>();

        services.AddDbContext<DbContext, TestDbContext>(options =>
        {
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
        }, ServiceLifetime.Singleton);

        services.AddTransient<IUnitOfWork, Persistence.UnitOfWork>();

        services.AddSingleton<List<IDomainEvent>>();

        services.AddSingleton<IDomainEventTypeMapping>(_ => new DomainEventTypeMapping(assembly));

        services.AddMediatR(assembly);

        services.DecorateEventHandlersWithIdempotency();

        return services.BuildServiceProvider();
    }
}
