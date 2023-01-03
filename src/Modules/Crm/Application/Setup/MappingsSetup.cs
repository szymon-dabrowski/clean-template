using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Crm.Application.Setup;

internal static class MappingsSetup
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(typeof(AssemblyMarker).Assembly);

        services.AddSingleton(mappingConfig);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}