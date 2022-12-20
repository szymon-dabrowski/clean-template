using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Dto.Commands.Items;
public record DeleteItemCommand(Guid ItemId) : ICommand;