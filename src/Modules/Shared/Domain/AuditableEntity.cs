namespace Clean.Modules.Shared.Domain;
public class AuditableEntity<TId> : Entity<TId>, IAuditableEntity
    where TId : notnull
{
    protected AuditableEntity(TId id)
        : base(id)
    {
    }

    public Guid CreatedByUserId { get; protected set; }

    public DateTime CreatedDateUtc { get; protected set; }

    public Guid? ModifiedByUserId { get; protected set; }

    public DateTime? ModifiedDateUtc { get; protected set; }
}