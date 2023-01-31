using Clean.Modules.Shared.Common.Errors;

namespace Clean.Modules.Shared.Domain;

public abstract class ValueObject : IEquatable<ValueObject>
{
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

    protected abstract IEnumerable<object> GetEqualityComponents();
}