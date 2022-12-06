using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Shared.Infrastructure;

public interface IModuleServiceProvider
{
    IServiceScope BeginLifetimeScope();
}
