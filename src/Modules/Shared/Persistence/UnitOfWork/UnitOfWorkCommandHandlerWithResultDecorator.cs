using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Shared.Persistence.UnitOfWork;
public class UnitOfWorkCommandHandlerWithResultDecorator<TCommand, TResult>
    : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    private readonly ICommandHandler<TCommand, TResult> decorated;
    private readonly IUnitOfWork unitOfWork;

    public UnitOfWorkCommandHandlerWithResultDecorator(
        ICommandHandler<TCommand, TResult> decorated,
        IUnitOfWork unitOfWork)
    {
        this.decorated = decorated;
        this.unitOfWork = unitOfWork;
    }

    public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var result = await decorated.Handle(request, cancellationToken);

        await unitOfWork.Commit(cancellationToken);

        return result;
    }
}