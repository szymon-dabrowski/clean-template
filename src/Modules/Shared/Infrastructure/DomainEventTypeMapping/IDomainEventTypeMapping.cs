namespace Clean.Modules.Shared.Infrastructure.DomainEventTypeMapping;
public interface IDomainEventTypeMapping
{
    Type Map(string domainEventName);
}