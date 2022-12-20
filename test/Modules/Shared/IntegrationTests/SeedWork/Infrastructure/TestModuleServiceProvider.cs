using Clean.Modules.Shared.Infrastructure.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Infrastructure;
internal class TestModuleServiceProvider : IModuleServiceProvider
{
    private readonly IServiceProvider serviceProvider;

    public TestModuleServiceProvider(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public IServiceScope BeginLifetimeScope()
    {
        return serviceProvider.CreateScope();
    }
}