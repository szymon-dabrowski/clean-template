using Clean.Modules.Crm.Application.Customers.CreateCustomer;
using Clean.Modules.Crm.Application.Customers.Dto;
using Clean.Modules.Crm.Application.Items.CreateItem;
using Clean.Modules.Crm.Application.Items.Dto;
using Clean.Modules.Crm.Infrastructure.Module;

namespace Clean.Modules.Crm.IntegrationTests.Orders;
public sealed class OrdersTestsFixture : IDisposable
{
    public CustomerDto Customer { get; private set; } = null!;

    public ItemDto Item1 { get; private set; } = null!;

    public ItemDto Item2 { get; private set; } = null!;

    public async Task SeedWork(CrmModule crmModule)
    {
        if (Customer == null)
        {
            Customer = await InitCustomer(crmModule);
        }

        if (Item1 == null)
        {
            Item1 = await InitItem1(crmModule);
        }

        if (Item2 == null)
        {
            Item2 = await InitItem2(crmModule);
        }
    }

    public void Dispose()
    {
        // cleanup
    }

    private async Task<CustomerDto> InitCustomer(CrmModule crmModule)
    {
        var command = new CreateCustomerCommand(
            Name: "Order Tests Customer",
            TaxId: "Order Tests Customer - Tax Id",
            Address: "Address",
            PostalCode: "00-123",
            City: "City",
            Phones: new List<string>() { "333444555" },
            Emails: new List<string>() { "ordercustomer@domain.com" });

        var result = await crmModule.ExecuteCommand(command);

        return new CustomerDto(
            result.Value,
            command.Name,
            command.TaxId,
            command.Address,
            command.PostalCode,
            command.City,
            command.Phones,
            command.Emails);
    }

    private async Task<ItemDto> InitItem1(CrmModule crmModule)
    {
        var command = new CreateItemCommand(
            Name: "Order Tests Item 1",
            Description: "Order Tests Item 1 Description",
            BasePrice: 111,
            BaseCurrency: "USD");

        var result = await crmModule.ExecuteCommand(command);

        return new ItemDto(
            result.Value,
            command.Name,
            command.Description,
            command.BasePrice,
            command.BaseCurrency);
    }

    private async Task<ItemDto> InitItem2(CrmModule crmModule)
    {
        var command = new CreateItemCommand(
            Name: "Order Tests Item 2",
            Description: "Order Tests Item 2 Description",
            BasePrice: 222,
            BaseCurrency: "USD");

        var result = await crmModule.ExecuteCommand(command);

        return new ItemDto(
            result.Value,
            command.Name,
            command.Description,
            command.BasePrice,
            command.BaseCurrency);
    }
}