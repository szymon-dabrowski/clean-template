namespace Clean.Modules.Shared.Domain;
public interface IAuditableEntity
{
    public Guid CreatedByUserId { get; }

    public DateTime CreatedDateUtc { get; }

    public Guid? ModifiedByUserId { get; }

    public DateTime? ModifiedDateUtc { get; }
}