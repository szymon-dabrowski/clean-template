using Clean.Modules.Crm.Application.Customers.Dto;
using FluentAssertions;

namespace Clean.Modules.Crm.IntegrationTests.Customers;
internal static class CustomersTestsExtensions
{
    public static void ShouldBe(this CustomerDto? actualCustomer, CustomerDto expectedCustomer)
    {
        actualCustomer.Should().NotBeNull();
        actualCustomer!.Name.Should().Be(expectedCustomer!.Name);
        actualCustomer.Id.Should().NotBeEmpty();
        actualCustomer.TaxId.Should().Be(expectedCustomer.TaxId);
        actualCustomer.Address.Should().Be(expectedCustomer.Address);
        actualCustomer.PostalCode.Should().Be(expectedCustomer.PostalCode);
        actualCustomer.City.Should().Be(expectedCustomer.City);
        actualCustomer.Phones.Should().HaveCount(expectedCustomer.Phones.Count);
        actualCustomer.Phones.Should().BeEquivalentTo(expectedCustomer.Phones);
        actualCustomer.Emails.Should().HaveCount(expectedCustomer.Emails.Count);
        actualCustomer.Emails.Should().BeEquivalentTo(expectedCustomer.Emails);
    }
}