using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Shared.Persistence.UnitOfWork;
public class UnitOfWorkCommandHandlerDecorator<TCommand>
    : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    private readonly ICommandHandler<TCommand> decorated;
    private readonly IUnitOfWork unitOfWork;

    public UnitOfWorkCommandHandlerDecorator(
        ICommandHandler<TCommand> decorated,
        IUnitOfWork unitOfWork)
    {
        this.decorated = decorated;
        this.unitOfWork = unitOfWork;
    }

    public async Task Handle(TCommand request, CancellationToken cancellationToken)
    {
        await decorated.Handle(request, cancellationToken);

        await unitOfWork.Commit(cancellationToken);
    }
}