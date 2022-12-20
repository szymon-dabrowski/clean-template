using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Application.Commands.CreateTestAggregateRoot;
internal class CreateTestAggregateRootCommandHandler
    : ICommandHandler<CreateTestAggregateRootCommand, ErrorOr<TestAggregateRoot>>
{
    private readonly ITestAggregateRootRepository repository;

    public CreateTestAggregateRootCommandHandler(
        ITestAggregateRootRepository repository)
    {
        this.repository = repository;
    }

    public async Task<ErrorOr<TestAggregateRoot>> Handle(
        CreateTestAggregateRootCommand request,
        CancellationToken cancellationToken)
    {
        var testAggregateRoot = TestAggregateRoot.Create(request.TestProperty);

        await repository.Add(testAggregateRoot);

        return testAggregateRoot;
    }
}