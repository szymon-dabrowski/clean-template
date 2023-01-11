using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Domain.Customers.Services;
using Clean.Modules.Shared.Common.Errors;
using FluentAssertions;
using Moq;
using Xunit;

namespace Clean.Modules.Crm.UnitTests.Customers;

public class CustomerTests
{
    [Fact]
    public async Task CreateCustomer_CustomerIsUnique_CustomerCreated()
    {
        var name = "testCustomer";
        var taxId = "testCustomerTaxId";
        var address = "testAddress";
        var postalCode = "12345";
        var city = "testCity";
        var phones = new List<string> { "1234567890", "1234567891" };
        var emails = new List<string> { "email1@domain.com", "email2@domain.com" };

        var customerNameUniquenessChecker = MockCustomerNameUniquenessChecker(withResult: true);
        var customerTaxIdUniquenessChecker = MockCustomerTaxIdUniquenessChecker(withResult: true);

        var customer = await Customer.Create(
            name,
            taxId,
            address,
            postalCode,
            city,
            phones,
            emails,
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        customer.IsError.Should().BeFalse();
        customer.Value.Should().NotBeNull();
        customer.Value.Name.Should().Be(name);
        customer.Value.TaxId.Should().Be(taxId);
        customer.Value.Address.Should().Be(address);
        customer.Value.PostalCode.Should().Be(postalCode);
        customer.Value.City.Should().Be(city);
        customer.Value.Phones.Should().HaveCount(2);
        customer.Value.Emails.Should().HaveCount(2);
    }

    [Fact]
    public async Task CreateCustomer_CustomerNameIsNotUnique_ErrorReturned()
    {
        var customerNameUniquenessChecker = MockCustomerNameUniquenessChecker(withResult: false);
        var customerTaxIdUniquenessChecker = MockCustomerTaxIdUniquenessChecker(withResult: true);
        var customer = await CreateTestCustomer(
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        customer.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task CreateCustomer_CustomerTaxIdIsNotUnique_ErrorReturned()
    {
        var customerNameUniquenessChecker = MockCustomerNameUniquenessChecker(withResult: true);
        var customerTaxIdUniquenessChecker = MockCustomerTaxIdUniquenessChecker(withResult: false);
        var customer = await CreateTestCustomer(
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        customer.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCustomer_CustomerIsUnique_CustomerUpdated()
    {
        var customerNameUniquenessChecker = MockCustomerNameUniquenessChecker(withResult: true);
        var customerTaxIdUniquenessChecker = MockCustomerTaxIdUniquenessChecker(withResult: true);
        var customer = await CreateTestCustomer(
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        var name = "testCustomer";
        var taxId = "testCustomerTaxId";
        var address = "testAddress";
        var postalCode = "12345";
        var city = "testCity";
        var phones = new List<string> { "1234567890", "1234567891" };
        var emails = new List<string> { "email1@domain.com", "email2@domain.com" };

        await customer.Value.Update(
            name,
            taxId,
            address,
            postalCode,
            city,
            phones,
            emails,
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        customer.IsError.Should().BeFalse();
        customer.Value.Should().NotBeNull();
        customer.Value.Name.Should().Be(name);
        customer.Value.TaxId.Should().Be(taxId);
        customer.Value.Address.Should().Be(address);
        customer.Value.PostalCode.Should().Be(postalCode);
        customer.Value.City.Should().Be(city);
        customer.Value.Phones.Should().HaveCount(2);
        customer.Value.Emails.Should().HaveCount(2);
    }

    [Fact]
    public async Task UpdateCustomer_CustomerIsDeleted_ErrorReturned()
    {
        var customerNameUniquenessChecker = MockCustomerNameUniquenessChecker(withResult: true);
        var customerTaxIdUniquenessChecker = MockCustomerTaxIdUniquenessChecker(withResult: true);
        var customer = await CreateTestCustomer(
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        customer.Value.Delete();

        var updateResult = await customer.Value.Update(
            "name",
            "taxId",
            "address",
            "postalCode",
            "city",
            phones: new List<string>(),
            emails: new List<string>(),
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        updateResult.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCustomer_NameAlreadyExists_ErrorReturned()
    {
        var successCustomerNameUniquenessChecker = MockCustomerNameUniquenessChecker(withResult: true);
        var failingCustomerNameUniquenessChecker = MockCustomerNameUniquenessChecker(withResult: false);
        var customerTaxIdUniquenessChecker = MockCustomerTaxIdUniquenessChecker(withResult: true);

        var customer = await CreateTestCustomer(
            successCustomerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        var updateResult = await customer.Value.Update(
            "newName",
            "test",
            "test",
            "test",
            "test",
            phones: new List<string>(),
            emails: new List<string>(),
            failingCustomerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        updateResult.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCustomer_TaxIdAlreadyExists_ErrorReturned()
    {
        var customerNameUniquenessChecker = MockCustomerNameUniquenessChecker(withResult: true);
        var successCustomerTaxIdUniquenessChecker = MockCustomerTaxIdUniquenessChecker(withResult: true);
        var failingCustomerTaxIdUniquenessChecker = MockCustomerTaxIdUniquenessChecker(withResult: false);

        var customer = await CreateTestCustomer(
            customerNameUniquenessChecker,
            successCustomerTaxIdUniquenessChecker);

        var updateResult = await customer.Value.Update(
            "test",
            "newTaxId",
            "test",
            "test",
            "test",
            phones: new List<string>(),
            emails: new List<string>(),
            customerNameUniquenessChecker,
            failingCustomerTaxIdUniquenessChecker);

        updateResult.IsError.Should().BeTrue();
    }

    private ICustomerNameUniquenessChecker MockCustomerNameUniquenessChecker(bool withResult)
    {
        var customerNameUniquenessChecker = new Mock<ICustomerNameUniquenessChecker>();

        customerNameUniquenessChecker
            .Setup(c => c.IsUnique(It.IsAny<string>()))
            .Returns<string>(_ => Task.FromResult(withResult));

        return customerNameUniquenessChecker.Object;
    }

    private ICustomerTaxIdUniquenessChecker MockCustomerTaxIdUniquenessChecker(bool withResult)
    {
        var customerTaxIdUniquenessChecker = new Mock<ICustomerTaxIdUniquenessChecker>();

        customerTaxIdUniquenessChecker
            .Setup(c => c.IsUnique(It.IsAny<string>()))
            .Returns<string>(_ => Task.FromResult(withResult));

        return customerTaxIdUniquenessChecker.Object;
    }

    private async Task<ErrorOr<Customer>> CreateTestCustomer(
        ICustomerNameUniquenessChecker customerNameUniquenessChecker,
        ICustomerTaxIdUniquenessChecker customerTaxIdUniquenessChecker)
    {
        var customer = await Customer.Create(
            name: "test",
            taxId: "test",
            address: "test",
            postalCode: "test",
            city: "test",
            phones: new List<string>(),
            emails: new List<string>(),
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        return customer;
    }
}