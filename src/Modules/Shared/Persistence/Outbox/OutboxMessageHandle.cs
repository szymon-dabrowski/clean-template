namespace Clean.Modules.Shared.Persistence.Outbox;

public class OutboxMessageHandle
{
    public Guid Id { get; set; }
    public string HandledBy { get; set; } = string.Empty;
    public DateTime HandledOn { get; set; }
}
