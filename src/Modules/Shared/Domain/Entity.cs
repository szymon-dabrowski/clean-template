namespace Clean.Modules.Shared.Domain;
#pragma warning disable S4035 // Classes implementing "IEquatable<T>" should be sealed
public class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
#pragma warning restore S4035 // Classes implementing "IEquatable<T>" should be sealed
{
    protected Entity(TId id)
    {
        Id = id;
    }

    public TId Id { get; protected set; }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    protected static bool EqualOperator(Entity<TId> left, Entity<TId> right)
    {
        if (left is null ^ right is null)
        {
            return false;
        }

        return ReferenceEquals(left, right) || (left?.Equals(right) ?? false);
    }

    protected static bool NotEqualOperator(Entity<TId> left, Entity<TId> right)
    {
        return !EqualOperator(left, right);
    }
}