using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Money;
public class PriceValueObject : ValueObject
{
    private PriceValueObject(
        decimal value,
        string currency)
    {
        Value = value;
        Currency = currency;
    }

    public string Currency { get; private set; }

    public decimal Value { get; private set; }

    public static PriceValueObject Create(
        decimal value,
        string currency)
    {
        return new PriceValueObject(value, currency);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }
}