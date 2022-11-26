﻿using Clean.Modules.Shared.Infrastructure.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.UserAccess.Infrastructure.Module;

public class UserAccessServiceProvider : IModuleServiceProvider
{
    private static IServiceProvider serviceProvider =
        new ServiceCollection().BuildServiceProvider();

    public static void Build(IServiceCollection services)
    {
        serviceProvider = services.BuildServiceProvider();
    }

    public IServiceScope BeginLifetimeScope()
    {
        return serviceProvider.CreateScope();
    }
}
