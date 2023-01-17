using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Modules.Shared.Application.Setup;

public static class MappingsSetup
{
    public static IServiceCollection AddMappings(this IServiceCollection services, Assembly assembly)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;

        mappingConfig.Scan(assembly);

        services.AddSingleton(mappingConfig);

        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}