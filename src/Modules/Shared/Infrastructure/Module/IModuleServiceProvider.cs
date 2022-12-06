using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Shared.Infrastructure.Module;

public interface IModuleServiceProvider
{
    IServiceScope BeginLifetimeScope();
}