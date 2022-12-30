using Clean.Modules.Crm.Domain.Customers.Rules;
using Clean.Modules.Crm.Domain.Customers.Services;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Customers;

public class Customer : AggregateRoot<Guid>
{
    private Customer()
        : base(Guid.Empty)
    {
    }

    private Customer(
        Guid id,
        string name,
        string taxId,
        string address,
        string postalCode,
        string city,
        List<string> phones,
        List<string> emails)
        : base(id)
    {
        Name = name;
        Address = address;
        PostalCode = postalCode;
        City = city;
        TaxId = taxId;
        Phones = phones;
        Emails = emails;
    }

    public string Name { get; private set; } = string.Empty;

    public string Address { get; private set; } = string.Empty;

    public string PostalCode { get; private set; } = string.Empty;

    public string City { get; private set; } = string.Empty;

    public string TaxId { get; private set; } = string.Empty;

    public bool IsDeleted { get; private set; }

    public IReadOnlyCollection<string> Phones { get; private set; }
        = Array.Empty<string>();

    public IReadOnlyCollection<string> Emails { get; private set; }
        = Array.Empty<string>();

    public static async Task<ErrorOr<Customer>> Create(
        string name,
        string taxId,
        string address,
        string postalCode,
        string city,
        List<string> phones,
        List<string> emails,
        ICustomerNameUniquenessChecker customerNameUniquenessService,
        ICustomerTaxIdUniquenessChecker customerTaxIdUniquenessChecker)
    {
        var result = await Check(
            new CustomerNameMustBeUniqueRule(name, customerNameUniquenessService),
            new CustomerTaxIdMustBeUniqueRule(taxId, customerTaxIdUniquenessChecker));

        if (result.IsError)
        {
            return Error.From(result);
        }

        return new Customer(
            Guid.NewGuid(),
            name,
            taxId,
            address,
            postalCode,
            city,
            phones,
            emails);
    }

    public async Task<ErrorOr<Customer>> Update(
        string name,
        string taxId,
        string address,
        string postalCode,
        string city,
        List<string> phones,
        List<string> emails,
        ICustomerNameUniquenessChecker customerNameUniquenessService,
        ICustomerTaxIdUniquenessChecker customerTaxIdUniquenessChecker)
    {
        var rulesToCheck = new List<IBussinesRule>()
        {
            new CannotUpdateDeletedCustomerRule(IsDeleted),
        };

        if (Name != name)
        {
            rulesToCheck.Add(new CustomerNameMustBeUniqueRule(
                name,
                customerNameUniquenessService));
        }

        if (TaxId != taxId)
        {
            rulesToCheck.Add(new CustomerTaxIdMustBeUniqueRule(
                taxId,
                customerTaxIdUniquenessChecker));
        }

        var result = await Check(rulesToCheck.ToArray());

        if (result.IsError)
        {
            return Error.From(result);
        }

        Name = name;
        TaxId = taxId;
        Address = address;
        PostalCode = postalCode;
        City = city;
        Phones = phones;
        Emails = emails;

        return this;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}