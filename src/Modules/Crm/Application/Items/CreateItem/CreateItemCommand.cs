using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;

namespace Clean.Modules.Crm.Application.Items.CreateItem;
public record CreateItemCommand(
    string Name,
    string Description,
    decimal BasePrice,
    string BaseCurrency)
    : ICommand<ErrorOr<ItemId>>;