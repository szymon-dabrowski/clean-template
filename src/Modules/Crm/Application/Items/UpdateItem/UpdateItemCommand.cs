using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.Crm.Application.Items.UpdateItem;
public record UpdateItemCommand(
    Guid ItemId,
    string Name,
    string Description,
    decimal BasePrice,
    string BaseCurrency)
    : ICommand<ErrorOr<Unit>>;