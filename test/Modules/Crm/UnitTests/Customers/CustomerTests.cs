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
    public async Task CreatingCustomer_WhenCustomerIsUnique_ShouldReturnCreatedCustomer()
    {
        var name = "testCustomer";
        var taxId = "testCustomerTaxId";
        var address = "testAddress";
        var postalCode = "12345";
        var city = "testCity";
        var phones = new string[] { "1234567890", "1234567891" };
        var emails = new string[] { "email1@domain.com", "email2@domain.com" };

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
    public async Task CreatingCustomer_WhenCustomerNameIsNotUnique_ShouldReturnError()
    {
        var customerNameUniquenessChecker = MockCustomerNameUniquenessChecker(withResult: false);
        var customerTaxIdUniquenessChecker = MockCustomerTaxIdUniquenessChecker(withResult: true);
        var customer = await CreateTestCustomer(
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        customer.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task CreatingCustomer_WhenCustomerTaxIdIsNotUnique_ShouldReturnError()
    {
        var customerNameUniquenessChecker = MockCustomerNameUniquenessChecker(withResult: true);
        var customerTaxIdUniquenessChecker = MockCustomerTaxIdUniquenessChecker(withResult: false);
        var customer = await CreateTestCustomer(
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        customer.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task UpdatingCustomer_WhenCustomerIsUnique_ShouldReturnUpdatedCustomer()
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
        var phones = new string[] { "1234567890", "1234567891" };
        var emails = new string[] { "email1@domain.com", "email2@domain.com" };

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
    public async Task UpdatingCustomer_WhenCustomerIsDeleted_ShouldReturnErrorAsync()
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
            phones: Array.Empty<string>(),
            emails: Array.Empty<string>(),
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        updateResult.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task UpdatingCustomer_WhenNameAlreadyExists_ShouldReturnErrorAsync()
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
            phones: Array.Empty<string>(),
            emails: Array.Empty<string>(),
            failingCustomerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        updateResult.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task UpdatingCustomer_WhenTaxIdAlreadyExists_ShouldReturnErrorAsync()
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
            phones: Array.Empty<string>(),
            emails: Array.Empty<string>(),
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
            phones: Array.Empty<string>(),
            emails: Array.Empty<string>(),
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        return customer;
    }
}