using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain;
using MediatR;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Application.Commands.DeleteTestAggregateRoot;
internal class DeleteTestAggregateRootCommandHandler
    : ICommandHandler<DeleteTestAggregateRootCommand>
{
    private readonly ITestAggregateRootRepository repository;

    public DeleteTestAggregateRootCommandHandler(
        ITestAggregateRootRepository repository)
    {
        this.repository = repository;
    }

    public async Task Handle(
        DeleteTestAggregateRootCommand request,
        CancellationToken cancellationToken)
    {
        await repository.Delete(request.TestAggregateRootId);
    }
}