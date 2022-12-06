using Clean.Modules.Shared.Domain;
using System.Reflection;

namespace Clean.Modules.Shared.Infrastructure.DomainEventTypeMapping;
public class DomainEventTypeMapping : IDomainEventTypeMapping
{
    private readonly IDictionary<string, Type> domainEventTypes;

    public DomainEventTypeMapping(Assembly domainAssembly)
    {
        domainEventTypes = domainAssembly.GetTypes()
            .Where(t => typeof(IDomainEvent).IsAssignableFrom(t))
            .ToDictionary(t => t.Name, t => t);
    }

    public Type Map(string domainEventName)
    {
        return domainEventTypes[domainEventName];
    }
}