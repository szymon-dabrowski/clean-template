using System.Reflection;

namespace Clean.Modules.Shared.Common.Enumerations;
public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();

    protected Enumeration(int value, string name) => (Value, Name) = (value, name);

    public static IReadOnlyCollection<TEnum> All => Enumerations.Values;

    public string Name { get; private set; }

    public int Value { get; private set; }

    public static TEnum? FromValue(int value)
        => Enumerations.TryGetValue(value, out var enumeration) ? enumeration : default;

    public static TEnum? FromName(string name)
        => Enumerations.Values.SingleOrDefault(e => e.Name == name);

    public override string ToString() => Name;

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() && Value == other.Value;
    }

    public override bool Equals(object? obj)
        => obj is Enumeration<TEnum> other && Equals(other);

    public override int GetHashCode() => Value.GetHashCode();

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fields = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(f => enumerationType.IsAssignableFrom(f.FieldType))
            .Select(f => (TEnum)f.GetValue(default)!);

        return fields.ToDictionary(v => v.Value);
    }
}