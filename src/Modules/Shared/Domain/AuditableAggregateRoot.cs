namespace Clean.Modules.Shared.Domain;
public class AuditableAggregateRoot<TId> : AuditableEntity<TId>, IAggregateRoot
    where TId : notnull
{
    private readonly List<IDomainEvent> domainEvents = new();

    protected AuditableAggregateRoot(TId id)
        : base(id)
    {
    }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => domainEvents.ToArray();

    public void ClearDomainEvents() => domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }
}