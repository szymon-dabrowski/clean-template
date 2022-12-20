using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;

namespace Clean.Modules.Crm.Dto.Commands.Items;
public record CreateItemCommand(
    string Name,
    string Description,
    decimal BasePrice,
    string BaseCurrency)
    : ICommand<ErrorOr<Guid>>;