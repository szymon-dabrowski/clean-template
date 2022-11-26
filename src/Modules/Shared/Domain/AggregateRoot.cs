namespace Clean.Modules.Shared.Domain;
public class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    where TId : notnull
{
    private readonly List<IDomainEvent> domainEvents = new();

    protected AggregateRoot(TId id) : base(id)
    {
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => domainEvents;

    public void ClearDomainEvents() => domainEvents.Clear();
}
