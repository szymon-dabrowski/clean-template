using Clean.Modules.Crm.Application.Customers.CreateCustomer;
using Clean.Modules.Crm.Application.Customers.DeleteCustomer;
using Clean.Modules.Crm.Application.Customers.Dto;
using Clean.Modules.Crm.Application.Customers.GetCustomer;
using Clean.Modules.Crm.Application.Customers.GetCustomers;
using Clean.Modules.Crm.Application.Customers.UpdateCustomer;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Modules.Crm.IntegrationTests.SeedWork;
using FluentAssertions;
using Xunit;

namespace Clean.Modules.Crm.IntegrationTests.Customers;

public class CustomerTests : IClassFixture<CrmStartupFixture>
{
    private readonly CrmModule crmModule;

    public CustomerTests(CrmStartupFixture crmStartupFixture)
    {
        crmModule = crmStartupFixture.CrmModule;
    }

    [Fact]
    public async Task CreateCustomer_CustomerDataValid_CustomerCreated()
    {
        var expectedCustomer = new CustomerDto(
            Id: Guid.Empty,
            Name: "Customer Name",
            TaxId: "000-123-321",
            Address: "Some Street",
            PostalCode: "00-000",
            City: "City",
            Phones: new List<string>() { "111222333" },
            Emails: new List<string>() { "email@domain.com" });

        var result = await crmModule.ExecuteCommand(new CreateCustomerCommand(
            expectedCustomer.Name,
            expectedCustomer.TaxId,
            expectedCustomer.Address,
            expectedCustomer.PostalCode,
            expectedCustomer.City,
            expectedCustomer.Phones,
            expectedCustomer.Emails));

        var customers = await crmModule.ExecuteQuery(new GetCustomersQuery());
        var customer = customers.First(c => c.Id == result.Value);

        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeEmpty();
        customers.Should().NotBeEmpty();
        customer.ShouldBe(expectedCustomer);
    }

    [Fact]
    public async Task UpdateCustomer_CustomerDataValid_CustomerUpdated()
    {
        var result = await crmModule.ExecuteCommand(new CreateCustomerCommand(
            Name: "Customer to update",
            TaxId: "Customer to update - tax id",
            Address: "Address",
            PostalCode: "00-000",
            City: "City",
            Phones: new List<string>() { },
            Emails: new List<string>() { }));

        var expectedCustomer = new CustomerDto(
            Id: result.Value,
            Name: "Updated Name",
            TaxId: "123-123-123",
            Address: "Some Street",
            PostalCode: "00-000",
            City: "City",
            Phones: new List<string>() { "111222333" },
            Emails: new List<string>() { "email@domain.com" });

        var updateResult = await crmModule.ExecuteCommand(new UpdateCustomerCommand(
            expectedCustomer.Id,
            expectedCustomer.Name,
            expectedCustomer.TaxId,
            expectedCustomer.Address,
            expectedCustomer.PostalCode,
            expectedCustomer.City,
            expectedCustomer.Phones,
            expectedCustomer.Emails));

        var customer = await crmModule.ExecuteQuery(
            new GetCustomerQuery(result.Value));

        updateResult.IsError.Should().BeFalse();

        customer.ShouldBe(expectedCustomer);
    }

    [Fact]
    public async Task DeleteCustomer_CustomerExits_CustomerDeleted()
    {
        var result = await crmModule.ExecuteCommand(new CreateCustomerCommand(
            Name: "Customer to delete",
            TaxId: "Customer to delete - taxId",
            Address: "Address",
            PostalCode: "00-000",
            City: "City",
            Phones: new List<string>() { },
            Emails: new List<string>() { }));

        await crmModule.ExecuteCommand(new DeleteCustomerCommand(result.Value));

        var customer = await crmModule
            .ExecuteQuery(new GetCustomerQuery(result.Value));

        customer.Should().BeNull();
    }
}