using Clean.Modules.Shared.Common.Errors;

namespace Clean.Modules.Shared.Domain;
public class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
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

    protected static async Task<ErrorOr<bool>> Check(params IBussinesRule[] rules)
    {
        foreach (var rule in rules)
        {
            var isBroken = await rule.IsBroken();

            if (isBroken)
            {
                return Error.BusinessRuleBroken(description: rule.Message);
            }
        }

        return true;
    }
}