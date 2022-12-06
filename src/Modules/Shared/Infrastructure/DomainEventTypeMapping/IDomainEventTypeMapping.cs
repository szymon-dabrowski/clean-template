namespace Clean.Modules.Shared.Infrastructure;
public interface IDomainEventTypeMapping
{
    Type Map(string domainEventName);
}
