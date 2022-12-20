using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Application.Commands.DeleteTestAggregateRoot;
internal record DeleteTestAggregateRootCommand(
    Guid TestAggregateRootId)
    : ICommand;