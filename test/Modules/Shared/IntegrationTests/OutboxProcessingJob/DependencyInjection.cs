using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Domain;
using Clean.Modules.Shared.Infrastructure.DomainEventTypeMapping;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Infrastructure;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Clean.Modules.Shared.IntegrationTests.OutboxProcessingJob;
internal static class DependencyInjection
{
    public static IServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IDateTimeProvider, TestDateTimeProvider>();

        services.AddDbContext<DbContext, TestDbContext>(
            options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            },
            ServiceLifetime.Singleton);

        services.AddSingleton<List<IDomainEvent>>();

        services.AddSingleton<IDomainEventTypeMapping>(_ =>
            new DomainEventTypeMapping(typeof(DependencyInjection).Assembly));

        services.AddTransient(s =>
        {
            var inMemoryEventsStore = s.GetRequiredService<List<IDomainEvent>>();

            var mock = new Mock<IPublisher>();

            mock.Setup(p => p.Publish(It.IsAny<IDomainEvent>(), default))
                .Callback<IDomainEvent, CancellationToken>((p, _) => inMemoryEventsStore.Add(p));

            return mock.Object;
        });

        return services.BuildServiceProvider();
    }
}