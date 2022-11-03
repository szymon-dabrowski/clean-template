namespace Clean.Modules.Shared.Domain;
#pragma warning disable S4035 // Classes implementing "IEquatable<T>" should be sealed
public abstract class ValueObject : IEquatable<ValueObject>
#pragma warning restore S4035 // Classes implementing "IEquatable<T>" should be sealed
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (left is null ^ right is null)
        {
            return false;
        }
        return ReferenceEquals(left, right) || (left?.Equals(right) ?? false);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }
}
