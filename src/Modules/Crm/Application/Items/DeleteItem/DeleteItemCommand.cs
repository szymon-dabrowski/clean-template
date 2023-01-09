using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Application.Items.DeleteItem;
public record DeleteItemCommand(Guid ItemId) : ICommand;