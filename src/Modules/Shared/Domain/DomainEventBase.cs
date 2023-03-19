namespace Clean.Modules.Shared.Domain;
public record DomainEventBase : IDomainEvent
{
    public DomainEventBase()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }

    public Guid Id { get; }

    public DateTime OccurredOn { get; }
}