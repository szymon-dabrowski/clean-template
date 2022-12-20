using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Application.Commands.CreateTestAggregateRoot;
internal record CreateTestAggregateRootCommand(
    string TestProperty)
    : ICommand<ErrorOr<TestAggregateRoot>>;