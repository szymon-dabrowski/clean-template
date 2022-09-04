using Mapster;
using MapsterMapper;
using System.Reflection;

namespace Clean.API.Common.Mappings;

public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(mappingConfig);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
