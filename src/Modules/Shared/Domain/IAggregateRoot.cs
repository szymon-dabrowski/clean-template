namespace Clean.Modules.Shared.Domain;
public interface IAggregateRoot
{
    void ClearDomainEvents();
    IReadOnlyCollection<IDomainEvent> GetDomainEvents();
}
